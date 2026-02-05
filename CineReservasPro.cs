using System;

namespace CineReservasPro
{
	class Program
	{
		//Punto de entrada a la App
		public static void Main(string[] args)
		{

			//Try Catch

			try
			{
				//Deaclarar un arreglo unidimensional con la cartelera de peliculas
				String[] peliculas = { "Kimetsu no Yaiba", "POETA", "The Conjuring: Last Rites", "The Long Walk", "A Big Bold Beutiful Journey"};

				//Definimos Cantidad De Salas Disponibles (Cada sala tendra su propio mapa 2D de asientos)
				int numeroSalas = 3;

				int[][,] salas = new int[numeroSalas][,];

				for (int i = 0; i < numeroSalas; i++)
				{
					salas[i] = new int[7, 10];
				}

				decimal[] precioPorFila = { 12.0m, 12.0m, 16.0m, 16.0m, 20.0m, 20.0m, 22.0m };

				//Inicializamos acumuladores para estadisticas
				int totalBoletosVendidos = 0;
				decimal totalRecaudo = 0m;

				while (true)
				{
					Console.Clear();
					Console.WriteLine("BIENVENIDO A CINE RESERVAS PRO");

					Console.WriteLine("\nCartelera Disponible:");

					for (int i = 0; i < peliculas.Length; i++)
					{
						Console.WriteLine($"{i + 1}. {peliculas[i]}");
					}

					//Mostrar el menu

					Console.WriteLine("\nMenu Principal:");
					Console.WriteLine("1. Ver todos los asientos de una sala");
					Console.WriteLine("2. Reservar Asientos");
					Console.WriteLine("3. Cancelar Reserva");
					Console.WriteLine("4. Ver Resumen de Ventas");
					Console.WriteLine("5. Cambiar Precios Por Fila");
					Console.WriteLine("0. Salir del programa");

					int op = LeerEnteroSeguro("\nElige una Opción del menú", 0, 5);

					//Switch Case

					if (op == 0) //Salir Del Programa
					{
						Console.WriteLine("\nGracias por Usar Cine Reservas");
						Console.WriteLine("Saliendo del programa.........");
						break;

					}
					else if (op == 1) //Ver Asientos de una sala
					{
						//Pedimos Numero de sala a elegir
						int sala = LeerEnteroSeguro($"\n Elige una sala (1-{numeroSalas}):", 1, numeroSalas) - 1;
						Console.WriteLine($"\nEstado de la sala #{sala + 1}");
						MostrarSala(salas[sala]);

						Console.WriteLine("\nPresione cualquier tecla para continuar");
						Console.ReadKey(true);
					}
					else if (op == 2) //Reservar Asientos
					{
						int sala = LeerEnteroSeguro($"\n Elige una sala (1-{numeroSalas}):", 1, numeroSalas) - 1;
						int idxPeli = LeerEnteroSeguro($"\nElige Pelicula (1-{peliculas.Length}): ", 1, peliculas.Length);

						Console.WriteLine($"\nMapa de asientos de la sala {sala + 1} ({peliculas[idxPeli - 1]})");

						MostrarSala(salas[sala]);

						//Pregunatmos al usuario cuantos asientos desea reservar
						int cantidad = LeerEnteroSeguro($"\n¿Cuantos Asientos deseas reservar? (1-10)", 1, 10);

						//Acumulador
						decimal costoCompra = 0m;

						for (int r = 0; r < cantidad; r++)
						{
							Console.WriteLine($"Reserva #{r + 1} de {cantidad}");

							int fila = LeerEnteroSeguro("\nIngrese fila (0-6)", 0, salas[sala].GetLength(0) - 1);
							int columna = LeerEnteroSeguro("\nIngrese columna (0-9)", 0, salas[sala].GetLength(1) - 1);

							if (salas[sala][fila, columna] == 1)
							{
								Console.WriteLine("El asiento seleccionado se encuentra ocupado. Intente nuevamente");
								r--;
								continue;
							}

							salas[sala][fila, columna] = 1;

							//Sumamos el precio correspondiente 

							decimal preciosAsiento = precioPorFila[fila];
							costoCompra += preciosAsiento;

							totalBoletosVendidos++;
							totalRecaudo += preciosAsiento;

							Console.WriteLine($"Asiento Reservado (fila{fila},columna{columna}) - Precio ${preciosAsiento:0.00}");
							Console.WriteLine("\nMapa Actualizado");
							MostrarSala(salas[sala]);

							Console.WriteLine("\nPresione cualquier tecla para continuar");
							Console.ReadKey(true);
						}
					}
					else if (op == 3) //Cancelar Reserva
					{
						//Numero de sala a elegir
						Console.WriteLine("Cancelar Reserva: Eliga La sala donde se cancelará la reserva.");
						int sala = LeerEnteroSeguro($"\n Elige una sala (1-{numeroSalas}):", 1, numeroSalas) - 1;
						Console.WriteLine($"\nEstado de la sala #{sala + 1}");
						MostrarSala(salas[sala]);

						// Verificar si hay asientos ocupados en la sala
						int ocupadosSala = ContarOcupados(salas[sala]);
						if (ocupadosSala == 0)
						{
							Console.WriteLine("\nNo hay asientos ocupados en esta sala para cancelar.");
							Console.WriteLine("\nPresione cualquier tecla para continuar");
							Console.ReadKey(true);
							continue;
						}

						//Pregunatmos al usuario cuantos asientos desea cancelar
						int cantidad = LeerEnteroSeguro($"\n¿Cuantos Asientos desea cancelar? (1-{ocupadosSala})", 1, ocupadosSala);


						decimal costoTotal = 0m;

						for (int r = 0; r < cantidad; r++)
						{

							int fila = LeerEnteroSeguro("\nIngrese fila (0-6)", 0, salas[sala].GetLength(0) - 1);
							int columna = LeerEnteroSeguro("\nIngrese columna (0-9)", 0, salas[sala].GetLength(1) - 1);

							if (salas[sala][fila, columna] == 0)
							{
								Console.WriteLine("El asiento no esta ocupado eliga los asientos ocuapdos [X] que desea cancelar");
								r--;
								continue;
							}

							salas[sala][fila, columna] = 0;

							//Restamos el precio correspondiente 

							decimal preciosAsiento = precioPorFila[fila];
							costoTotal += preciosAsiento;

							totalBoletosVendidos--;
							totalRecaudo -= preciosAsiento;

							Console.WriteLine($"Asiento Cancelado (fila{fila},columna{columna}) - Reembolso ${preciosAsiento:0.00}");
							Console.WriteLine("\nMapa de la sala actualizado:");
							MostrarSala(salas[sala]);
							Console.WriteLine($"\nTotal reembolsado en esta operación: ${costoTotal:0.00}");

							Console.WriteLine("\nPresione cualquier tecla para continuar");
							Console.ReadKey(true);
						}

					}

					else if (op == 4) //Ver Resumen de Ventas
					{
						Console.WriteLine("\nResumen de Ventas");
						int ocupadosTotal = 0;

						for (int s = 0; s < numeroSalas; s++)
						{
							int ocupadosSala = ContarOcupados(salas[s]);
							Console.WriteLine($"Sala {s + 1}: {ocupadosSala} asientos ocupados.");
							ocupadosTotal += ocupadosSala;
						}

						Console.WriteLine($"Total boletos vendidos: {totalBoletosVendidos}");
						Console.WriteLine($"Total asientos ocupados: {ocupadosTotal}");
						Console.WriteLine($"Total recaudado: ${totalRecaudo:0.00}");

						Console.WriteLine("\nPresione cualquier tecla para continuar");
						Console.ReadKey(true);

					}
					else if (op == 5) //Cambiar Precios Por Fila
					{
						Console.WriteLine("\nPrecios actuales por fila:");
						for (int f = 0; f < precioPorFila.Length; f++)
						{
							Console.WriteLine($"Fila {f}: ${precioPorFila[f]:0.00}");
						}

						int filaACambiar = LeerEnteroSeguro($"\nIndica la fila a cambiar (0-{precioPorFila.Length - 1}): ", 0, precioPorFila.Length - 1);
						decimal nuevoPrecio = LeerDecimalSeguro($"Nuevo precio para la fila {filaACambiar} (0.00 - 30.00): ", 0m, 30m);

						precioPorFila[filaACambiar] = nuevoPrecio;
						Console.WriteLine($"\nPrecio actualizado: Fila {filaACambiar} -> ${nuevoPrecio:0.00}");

						Console.WriteLine("\nPresione cualquier tecla para continuar");
						Console.ReadKey(true);
					}
					else
					{
						Console.WriteLine("Opción no válida. Intente Nuevamente");
					}
  
					//Fin Switch Case

				}
			}
		
			//Catch

			catch (Exception)
			{
				Console.WriteLine("Ha ocurrido un error");
			}

		}

		//Metodos

		//Mostrar el mapa de asientos de una sala

		static void MostrarSala(int[,] sala)
		{
			//Encabezado de Columnas
			Console.Write(" ");
			for (int c = 0; c < sala.GetLength(1); c++)
			{
				Console.Write($"{c,3}");
			}
			Console.WriteLine();
			Console.WriteLine(new String('-', sala.GetLength(1) * 3 + 4));

			for (int f = 0; f < sala.GetLength(0); f++)
			{
				Console.Write($"{f,2}");
				for (int c = 0; c < sala.GetLength(1); c++)
				{
					char simbolo = sala[f, c] == 0 ? ' ' : 'X';
					Console.Write($"[{simbolo}]");
				}
				Console.WriteLine();
			}

			Console.WriteLine("\n[ ] = Vácio ");
			Console.WriteLine("[X] = Ocupado");
		}

		//Contar Asientos Ocupados en una sala

		static int ContarOcupados(int[,] sala)
		{
			int count = 0;

			for (int f = 0; f < sala.GetLength(0); f++)
			{
				for (int c = 0; c < sala.GetLength(1); c++)
				{
					if (sala[f, c] == 1)
					{
						count++;
					}
				}
			}
			return count;
		}

		//Leer un entero de forma segura dentro de un rango
		static int LeerEnteroSeguro(string mensaje, int minimo, int maximo)
		{
			while (true)
			{
				Console.WriteLine(mensaje);

				try
				{
					string? input = Console.ReadLine();
					if (input == null)
					{
						Console.WriteLine("Entrada no Válida (Vacía). Intente Nuevamente");
						continue;
					}

					int valor = int.Parse(input);

					if (valor < minimo || valor > maximo)
					{
						Console.WriteLine($"Entrada no válida. Debe estar estar entre {minimo} y {maximo}");
						continue;
					}
					return valor;
				}
				catch (FormatException)
				{
					Console.WriteLine("Debe Ingresar un número entero. Intente nuevamanete");
				}
				catch (OverflowException)
				{
					Console.WriteLine("Numero fuera de rango permitido por el sistema. Intenta Nuevamente");
				}
			}
		}

		//Leer un decimal de forma segura dentro de un rango
		static decimal LeerDecimalSeguro(string mensaje, decimal minimo, decimal maximo)
		{
			while (true)
			{
				Console.WriteLine(mensaje);

				try
				{
					string? input = Console.ReadLine();
					if (input == null)
					{
						Console.WriteLine("Entrada no Válida (Vacía). Intente Nuevamente");
						continue;
					}

					decimal valor = decimal.Parse(input);

					if (valor < minimo || valor > maximo)
					{
						Console.WriteLine($"Entrada no válida. Debe estar estar entre {minimo:0.00} y {maximo:0.00}");
						continue;
					}
					return valor;
				}
				catch (FormatException)
				{
					Console.WriteLine("Debe Ingresar un número decimal. Intenta nuevamanete");
				}
				catch (OverflowException)
				{
					Console.WriteLine("Numero fuera de rango permitido por el sistema. Intenta Nuevamente");
				}
			}
		}

		//Fin Metodos

	}

}
