using System.Globalization;

namespace AnalizadorDeNotas
{
    internal class Program
    {
        const double NotaMinima = 0.0;
        const double NotaMaxima = 5.0;
        const double NotaAprobatoria = 3.0;

        static void Main(string[] args)
        {
            Console.WriteLine("----- Analizador de Notas -----");

            int cantidad = LeerCantidadNotas();
            double[] notas = new double[cantidad];

            for (int i = 0; i < cantidad; i++)
            {
                notas[i] = LeerNota(i + 1);
            }

            double promedio = CalcularPromedio(notas);
            double mayor = ObtenerMayor(notas);
            double menor = ObtenerMenor(notas);
            int aprobadas = ContarAprobadas(notas);

            Console.WriteLine("\n----- Resultados -----");
            Console.WriteLine($"Cantidad de notas: {cantidad}");
            Console.WriteLine($"Promedio: {promedio:F2}");
            Console.WriteLine($"Nota mayor: {mayor:F1}");
            Console.WriteLine($"Nota menor: {menor:F1}");
            Console.WriteLine($"Notas aprobadas: {aprobadas}");
            Console.WriteLine($"Notas reprobadas: {cantidad - aprobadas}");
            Console.WriteLine($"Desempeño: {ClasificarPromedio(promedio)}");

            Console.WriteLine("\nPresione cualquier tecla para salir");
            Console.ReadKey();
        }

        static int LeerCantidadNotas()
        {
            while (true)
            {
                Console.WriteLine("¿Cuántas notas desea ingresar?");
                if (int.TryParse(Console.ReadLine(), out int cantidad) && cantidad > 0)
                {
                    return cantidad;
                }
                Console.WriteLine("Entrada invalida, Digite un numero mayor que 0");
            }
        }

        static double LeerNota(int numero)
        {
            while (true)
            {
                Console.WriteLine($"Nota {numero} ({NotaMinima:F1} - {NotaMaxima:F1}): ");
                string entrada = (Console.ReadLine() ?? "").Replace(',', '.');

                if (!double.TryParse(entrada, NumberStyles.Float, CultureInfo.InvariantCulture, out double nota))
                {
                    Console.WriteLine("Entrada invalida");
                    continue;
                }

                if (nota < NotaMinima || nota > NotaMaxima)
                {
                    Console.WriteLine($"La nota debe estar entre {NotaMinima:F1} y {NotaMaxima:F1}");
                    continue;
                }

                return nota;
            }
        }

        static double CalcularPromedio(double[] notas)
        {
            double suma = 0;
            for (int i = 0; i < notas.Length; i++)
            {
                suma += notas[i];
            }
            return suma / notas.Length;
        }

        static double ObtenerMayor(double[] notas)
        {
            double mayor = notas[0];
            for (int i = 1; i < notas.Length; i++)
            {
                if (notas[i] > mayor)
                {
                    mayor = notas[i];
                }
            }
            return mayor;
        }

        static double ObtenerMenor(double[] notas)
        {
            double menor = notas[0];
            for (int i = 1; i < notas.Length; i++)
            {
                if (notas[i] < menor)
                {
                    menor = notas[i];
                }
            }
            return menor;
        }

        static int ContarAprobadas(double[] notas)
        {
            int aprobadas = 0;
            for (int i = 0; i < notas.Length; i++)
            {
                if (notas[i] >= NotaAprobatoria)
                {
                    aprobadas++;
                }
            }
            return aprobadas;
        }

        static string ClasificarPromedio(double promedio)
        {
            if (promedio >= 4.5)
            {
                return "Superior";
            }
            else if (promedio >= 4.0)
            {
                return "Alto";
            }
            else if (promedio >= NotaAprobatoria)
            {
                return "Basico";
            }
            return "Bajo";
        }
    }
}
