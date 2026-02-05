
using System;

namespace SalasDeReunión
{
	class Program
	{
		public static void Main(string[] args)
		{
			//Inicio Try Catch

			try
			{
				//Bienvenida

				Console.WriteLine("Bienvenido a Sistema de Reservas de Salas de Reunión");

				//Arreglo unidimensional (Salas de Reunión)

				//1. Declarar un arreglo unidimensional con la cartelera de peliculas
				String[] nombresalas = { "Sala N° 1", "Sala N° 2", "Sala N° 3", "Sala N° 4", "Sala N° 5" };


				//2. Arreglo bidimensional de hoario
				//filas = días de la semana (5: lunes a viernes)
				// columnas = franjas horarias (4: 9:00–10:00, 10:00–11:00, etc.).
				int[,,] agenda = new int[nombresalas.Length, 5, 4];
				//Todos los días libres = 0
				//Dias ocupado = 1
				// Definimos la cantidad de salas que hay
				int numeroSalas = 5;
				int[][,] estadoSalas = new int[numeroSalas][,];
				for (int i = 0; i < numeroSalas; i++)
				{
					estadoSalas[i] = new int[5, 4];
				}

				//Menu (Bucle)

				while (true)
				{
			

					//Mostrar Salas Disponibles

					Console.WriteLine("Este Programa permite reservas las sala de Reunión No.1 - No.5");
					Console.WriteLine("Para Continuar al menu presione culquier Tecla");

					Console.ReadKey(true);

					Console.WriteLine("\nMenu De Opciones");
					Console.WriteLine("0.Salir del Programa");
					Console.WriteLine("1.Ver estado de una sala");
					Console.WriteLine("2.Reservar horario");
					Console.WriteLine("3.Cancelar reserva");
					Console.WriteLine("4.Resumen de reservas");



					//Switch Case para señeccionar opción de menú

					//Switch Case

					int op = LeerEnteroSeguro("\nElige una Opción del menú", 0, 5);


					if (op == 0) //Salir Del Programa
					{
						Console.WriteLine("\nGracias por Usar Salas De Reunion Reservas");
						Console.WriteLine("Saliendo del programa.........");
						break;

					}
					else if (op == 1) //Ver estado de una sala
					{
						int sala = LeerEnteroSeguro($"\n Elige una sala (1-{numeroSalas}):", 1, numeroSalas) - 1;
						Console.WriteLine($"\nEstado de la sala #{sala + 1}");
						VerEstadoSala(estadoSalas[sala]);

						Console.WriteLine("\nPresione cualquier tecla para continuar");
						Console.ReadKey(true);

					}
					else if (op == 2) //Reservar horario
					{
						int sala = LeerEnteroSeguro($"\n Elige una sala (1-{numeroSalas}):", 1, numeroSalas) - 1;

						Console.WriteLine($"\nMapa de horario de reservas de la sala {sala + 1} ({nombresalas[sala]})");

						VerEstadoSala(estadoSalas[sala]);

						//Pregunatmos al usuario cuantos horarios desea reservar
						int cantidad = LeerEnteroSeguro($"\n¿Cuantos horarios desea reservar? ", 0, 3);


						for (int r = 0; r < cantidad; r++)
						{
							Console.WriteLine($"Reserva #{r + 1} de {cantidad}");

							int fila = LeerEnteroSeguro("\nIngrese día (0=Lunes, 4=Viernes)", 0, 4);
							int columna = LeerEnteroSeguro("\nIngrese franja horaria (0 = 09:00, 3 = 12:00)", 0, 3);

							if (estadoSalas[sala][fila, columna] == 1)
							{
								Console.WriteLine("El horario seleccionado se encuentra ocupado. Intente nuevamente");
								r--;
								continue;
							}

							estadoSalas[sala][fila, columna] = 1;


						}
					}
					else if (op == 3) //Cancelar Reserva
					{
						//Numero de sala a elegir
						Console.WriteLine("Cancelar Reserva: Eliga La sala donde se cancelará la reserva.");
						int sala = LeerEnteroSeguro($"\n Elige una sala (1-{numeroSalas}):", 1, numeroSalas) - 1;
						Console.WriteLine($"\nEstado de la sala #{sala + 1}");
						VerEstadoSala(estadoSalas[sala]);

						// Verificar si hay horariis ocupados en la sala
						int ocupadosSala = ContarOcupados(estadoSalas[sala]);
						if (ocupadosSala == 0)
						{
							Console.WriteLine("\nNo hay horarios ocupados en esta sala para cancelar.");
							Console.WriteLine("\nPresione cualquier tecla para continuar");
							Console.ReadKey(true);
							continue;
						}

						//Pregunatmos al usuario cuantos horarios desea cancelar
						int cantidad = LeerEnteroSeguro($"\n¿Cuantos Horarios desea cancelar? (1-{ocupadosSala})", 1, ocupadosSala);

						for (int r = 0; r < cantidad; r++)
						{

							int fila = LeerEnteroSeguro("\nIngrese día (0=Lunes, 4=Viernes)", 0, 4);
							int columna = LeerEnteroSeguro("\nIngrese franja horaria (0 = 09:00, 3 = 12:00)", 0, 3);

							if (estadoSalas[sala][fila, columna] == 0)
							{
								Console.WriteLine("El horario no esta ocupado eliga los asientos ocuapdos [X] que desea cancelar");
								r--;
								continue;
							}

							estadoSalas[sala][fila, columna] = 0;

							

							Console.WriteLine($"Horario Cancelado (fila{fila},columna{columna})");
							Console.WriteLine("\nMapa de la sala actualizado:");
							VerEstadoSala(estadoSalas[sala]);
						
							Console.WriteLine("\nPresione cualquier tecla para continuar");
							Console.ReadKey(true);
						}
					}

					else if (op == 4) //Resumen de reservas
					{
						Console.WriteLine("\nResumen de Reservas por Sala");

						int totalReservas = 0;

						for (int s = 0; s < numeroSalas; s++)
						{
							int reservasSala = ContarOcupados(estadoSalas[s]);
							Console.WriteLine($"Sala {s + 1}: {reservasSala} horarios reservados.");
							totalReservas += reservasSala;
						}

						Console.WriteLine($"\nTotal de horarios reservados en todas las salas: {totalReservas}");
						Console.WriteLine("\nPresione cualquier tecla para continuar");
						Console.ReadKey(true);
					}


					else
					{
						Console.WriteLine("Opción no válida. Intente Nuevamente");
					}

				//Fin Switch Case

				}

				//Fin Bucle

			}

			//Fin Try


			//Catch
			catch (Exception)
			{
				Console.WriteLine("Ha Ocurrido Un Error");
			}
			//Fin Catch

		}


		//Metodos//

		//Ver Estado Sala
		static void VerEstadoSala(int[,] sala)
		{
			string[] dias = { "Lu", "Ma", "Mi", "Ju", "Vi" };
			string[] horas = { "09:00-10:00", "10:00-11:00", "11:00-12:00", "12:00-01:00" };

			Console.Write("             ");
			for (int c = 0; c < dias.Length; c++)
			{
				Console.Write($"[{dias[c]}]");
			}
			Console.WriteLine();

			for (int f = 0; f < horas.Length; f++)
			{
				Console.Write($"[{horas[f]}]");
				for (int d = 0; d < dias.Length; d++)
				{
					char estado = sala[d, f] == 0 ? ' ' : 'X';
					Console.Write($"[{estado}]");
				}
				Console.WriteLine();
			}

			Console.WriteLine("\n[X] = Ocupado, [ ] = Libre");
		}


		//Contar Ocupados

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


		//LeerUnEnteroDeFormaSegura
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

		//Fin Metodos

	}

}


