// CABINAS PRATICAS MUSICALES RESERVAS:

using System;

namespace Cabinas_Praticas_Musicales
{
	class Program
	{
		static void Main(string[] args)
		{

			//Try - Catch

			try
			{

				//Arreglo Uniodimensional

				String[] nombreCabinas = { "Cabina A", "Cabina B", "Cabina C", "Cabina D", "Cabina E", "Cabina F", "Cabina G", "Cabina H", "Cabina I", "Cabina J" };

				//Arreglo Bidimensional

				int filas = 5; //Horarios: 08:00 - 18:00
				int columnas = 7; // Dias: Lunes - Domingo

				int[,,] agenda = new int[10, filas, columnas];  // 10 Cabinas , 4 Horarios, 6 Dias

				//Arreglo Bidimensional

				int cantidadCabinas = 10;

				int[][,] estadoCabinas = new int[cantidadCabinas][,];

				for (int i = 0; i < cantidadCabinas; i++)
				{
					estadoCabinas[i] = new int[filas, columnas];
				}

				//Bienvenida

				Console.WriteLine("Bienvenido al programa de Reserva de Cabinas para Praticas Musicales");
				Console.WriteLine("Ingrese Cualquier tecla para iniciar.");
				Console.ReadKey(true);

				//Menu Principal

				while (true)
				{
					//Limpiar Pantalla

					Console.Clear();

					//Mostrar Opciones Disponibles al ususario (Cabinas)

					Console.WriteLine("\nCabinas Para Practica Musical: ");
					for (int i = 0; i < nombreCabinas.Length; i++)
					{
						Console.WriteLine($"{i + 1}. {nombreCabinas[i]}");
					}

					//Mostrar Menu Principal

					Console.WriteLine("\nMenu Principal: ");
					Console.WriteLine("1. Reservar Cabina");
					Console.WriteLine("2. Ver Estado de Cabinas");
					Console.WriteLine("3. Cancelar Reserva");
					Console.WriteLine("4. Estadisticas De Uso");
					Console.WriteLine("0. Salir del Programa");

					//Leer Opcion del Menu Principal

					int op = LeerEnteroSeguro("\nElige una Opción del menú", 0, 4);

					//Switch Opcion del Menu Principal

					if (op == 0)
					{
						Console.WriteLine("Gracias por usar el programa. ¡Hasta luego!");
						break; // Salir del bucle y terminar el programa
					}
					else if (op == 1)
					{
						int cabina = LeerEnteroSeguro($"\n Elige una sala (1-{cantidadCabinas}):", 1, cantidadCabinas) - 1;

						Console.WriteLine($"\nMapa de horario de reservas de la sala {cabina + 1} ({nombreCabinas[cabina]})");

						MostrarEstadoCabinas(estadoCabinas[cabina]);

						//Pregunatmos al usuario cuantos horarios desea reservar
						int cantidad = LeerEnteroSeguro($"\n¿Cuantos horarios desea reservar? ", 0, 4);

						for (int r = 0; r < cantidad; r++)
						{
							Console.WriteLine($"Reserva #{r + 1} de {cantidad}");

							int fila = LeerEnteroSeguro($"\nIngrese franja horaria (0-{filas - 1}):", 0, filas - 1);
							int columna = LeerEnteroSeguro($"\nIngrese día (0-{columnas - 1}, 0=Lunes):", 0, columnas - 1);

							if (estadoCabinas[cabina][fila, columna] == 1)
							{
								Console.WriteLine("El horario seleccionado se encuentra ocupado. Intente nuevamente");
								r--;
								continue;
							}

							estadoCabinas[cabina][fila, columna] = 1;
						}
					}
					else if (op == 2)
					{
						//Numero de sala a elegir
						int cabina = LeerEnteroSeguro($"\n Elige una sala (1-{cantidadCabinas}):", 1, cantidadCabinas) - 1;
						Console.WriteLine($"\nEstado de la sala #{cabina + 1}");
						MostrarEstadoCabinas(estadoCabinas[cabina]);

						Console.WriteLine("\nPresione cualquier tecla para continuar");
						Console.ReadKey(true);
					}
					else if (op == 3)
					{
						//Numero de sala a elegir
						Console.WriteLine("Cancelar Reserva: Eliga La sala donde se cancelará la reserva.");
						int cabina = LeerEnteroSeguro($"\n Elige una sala (1-{cantidadCabinas}):", 1, cantidadCabinas) - 1;
						Console.WriteLine($"\nEstado de la sala #{cabina + 1}");
						MostrarEstadoCabinas(estadoCabinas[cabina]);

						// Verificar si hay horariis ocupados en la sala
						int ocupadosSala = ContarOcupados(estadoCabinas[cabina]);
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

							int fila = LeerEnteroSeguro($"\nIngrese franja horaria (0-{filas - 1}):", 0, filas - 1);
							int columna = LeerEnteroSeguro($"\nIngrese día (0-{columnas - 1}, 0=Lunes):", 0, columnas - 1);

							if (estadoCabinas[cabina][fila, columna] == 0)
							{
								Console.WriteLine("El horario no esta ocupado eliga los asientos ocuapdos [X] que desea cancelar");
								r--;
								continue;
							}

							estadoCabinas[cabina][fila, columna] = 0;

							Console.WriteLine($"Horario Cancelado (fila{fila},columna{columna})");
							Console.WriteLine("\nMapa de la sala actualizado:");
							MostrarEstadoCabinas(estadoCabinas[cabina]);

							Console.WriteLine("\nPresione cualquier tecla para continuar");
							Console.ReadKey(true);
						}
					}
					else if (op == 4)
					{
						Console.WriteLine("\nResumen de Reservas por Sala");

						int totalReservas = 0;

						for (int s = 0; s < cantidadCabinas; s++)
						{
							int reservasSala = ContarOcupados(estadoCabinas[s]);
							Console.WriteLine($"Sala {s + 1}: {reservasSala} horarios reservados.");
							totalReservas += reservasSala;
						}

						Console.WriteLine($"\nTotal de horarios reservados en todas las salas: {totalReservas}");
						Console.WriteLine("\nPresione cualquier tecla para continuar");
						Console.ReadKey(true);
					}
					else
					{
						Console.WriteLine("Opción no válida. Intente de nuevo.");
					}

				}

			}

			catch (Exception)

			{
				Console.WriteLine("Ha ocurrido un error");

			}

		}
		//Fin Main

		//Metodos//Funciones//

		//Mostrar Estado Cabinas

		static void MostrarEstadoCabinas(int[,] cabina)
		{
			Console.WriteLine("\nEstado de las Cabinas:");

			String[] dias = { "L", "M", "X", "J", "V", "S", "D" };
			String [] horas = { "08:00-10:00", "10:00-12:00", "12:00-14:00", "14:00-16:00", "16:00-18:00" };

			Console.Write("             ");

			for (int c = 0; c < dias.Length; c++)
			{
				Console.Write($"[{dias[c]}]");
			}
			Console.WriteLine();

			for (int h = 0; h < horas.Length; h++)
			{
				Console.Write($"[{horas[h]}]");
				for (int d = 0; d < dias.Length; d++)
				{
					char estado = cabina[h, d] == 0 ? ' ' : 'X';
					Console.Write($"[{estado}]");
				}
				Console.WriteLine();
			}

			Console.WriteLine("\n[X] = Ocupado, [ ] = Libre");
		}


		//Contar Ocupados
		static int ContarOcupados(int[,] cabina)
		{
			int count = 0;

			for (int f = 0; f < cabina.GetLength(0); f++)
			{
				for (int c = 0; c < cabina.GetLength(1); c++)
				{
					if (cabina[f, c] == 1)
					{
						count++;
					}
				}
			}
			return count;
		}


		//Leer Entero Seguro
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
						Console.WriteLine("Error: Entrada nula. Intente de nuevo.");
						continue;
					}

					int valor = int.Parse(input);

					if (valor < minimo || valor > maximo)
					{
						Console.WriteLine($"Error: El numero debe estar entre {minimo} y {maximo}. Intente de nuevo.");
						continue;
					}
					else
					{
						return valor;
					}
				}
				catch (FormatException)
				{
					Console.WriteLine("Error: Debe ingresar un numero entero valido.");
				}
				catch (OverflowException)
				{
					Console.WriteLine("Error: El numero ingresado es demasiado grande o demasiado pequeno.");
				}
			}
		}
		 //Fin Metodos//Funciones//
	}
}
	
