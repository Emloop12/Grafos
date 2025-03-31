using System;
using System.Collections.Generic;

public class Grafo
{
    // Diccionario que almacena las listas de adyacencia del grafo.
    // La clave es el nodo y el valor es la lista de sus vecinos.
    private Dictionary<int, List<int>> listaAdyacencia;

    // Booleano que indica si el grafo es dirigido o no dirigido.
    private bool esDirigido;

    // Constructor de la clase Grafo.
    // Inicializa el diccionario de listas de adyacencia y el tipo de grafo.
    public Grafo(bool dirigido = false)
    {
        listaAdyacencia = new Dictionary<int, List<int>>();
        esDirigido = dirigido;
    }

    // Método para agregar un nodo al grafo.
    public void AgregarNodo(int nodo)
    {
        // Si el nodo no existe en el diccionario, lo agrega con una lista de vecinos vacía.
        if (!listaAdyacencia.ContainsKey(nodo))
        {
            listaAdyacencia[nodo] = new List<int>();
        }
    }

    // Método para agregar una arista al grafo.
    public void AgregarArista(int nodoOrigen, int nodoDestino)
    {
        // Asegura que ambos nodos existan en el grafo.
        AgregarNodo(nodoOrigen);
        AgregarNodo(nodoDestino);

        // Agrega el nodo destino a la lista de vecinos del nodo origen.
        listaAdyacencia[nodoOrigen].Add(nodoDestino);

        // Si el grafo no es dirigido, agrega el nodo origen a la lista de vecinos del nodo destino.
        if (!esDirigido)
        {
            listaAdyacencia[nodoDestino].Add(nodoOrigen);
        }
    }

    // Método para realizar un recorrido BFS (Búsqueda en Anchura) del grafo.
    public List<int> BFS(int nodoInicio)
    {
        // Lista para almacenar los nodos visitados.
        List<int> visitados = new List<int>();
        // Cola para almacenar los nodos a visitar.
        Queue<int> cola = new Queue<int>();

        // Marca el nodo inicial como visitado y lo agrega a la cola.
        visitados.Add(nodoInicio);
        cola.Enqueue(nodoInicio);

        // Mientras la cola no esté vacía, procesa los nodos.
        while (cola.Count > 0)
        {
            // Obtiene el siguiente nodo de la cola.
            int nodoActual = cola.Dequeue();

            // Recorre los vecinos del nodo actual.
            foreach (int vecino in listaAdyacencia[nodoActual])
            {
                // Si el vecino no ha sido visitado, lo marca como visitado y lo agrega a la cola.
                if (!visitados.Contains(vecino))
                {
                    visitados.Add(vecino);
                    cola.Enqueue(vecino);
                }
            }
        }

        // Retorna la lista de nodos visitados.
        return visitados;
    }

    // Método para realizar un recorrido DFS (Búsqueda en Profundidad) del grafo.
    public List<int> DFS(int nodoInicio)
    {
        // Lista para almacenar los nodos visitados.
        List<int> visitados = new List<int>();
        // Pila para almacenar los nodos a visitar.
        Stack<int> pila = new Stack<int>();

        // Agrega el nodo inicial a la pila.
        pila.Push(nodoInicio);

        // Mientras la pila no esté vacía, procesa los nodos.
        while (pila.Count > 0)
        {
            // Obtiene el siguiente nodo de la pila.
            int nodoActual = pila.Pop();

            // Si el nodo no ha sido visitado, lo marca como visitado.
            if (!visitados.Contains(nodoActual))
            {
                visitados.Add(nodoActual);

                // Recorre los vecinos del nodo actual y los agrega a la pila.
                foreach (int vecino in listaAdyacencia[nodoActual])
                {
                    pila.Push(vecino);
                }
            }
        }

        // Retorna la lista de nodos visitados.
        return visitados;
    }

    // Método para mostrar el grafo en la consola.
    public void MostrarGrafo()
    {
        // Recorre el diccionario de listas de adyacencia.
        foreach (var nodo in listaAdyacencia)
        {
            // Imprime el nodo y sus vecinos.
            Console.Write($"Nodo {nodo.Key}: ");
            foreach (var vecino in nodo.Value)
            {
                Console.Write($"{vecino} ");
            }
            Console.WriteLine();
        }
    }
}

public class Programa
{
    public static void Main(string[] args)
    {
        // Crea una instancia de la clase Grafo.
        Grafo grafo = new Grafo();

        // Bucle principal del programa.
        while (true)
        {
            Console.Clear(); // Limpio la pantalla
            // Imprime el menú de opciones.
            Console.WriteLine("\nMenú:");
            Console.WriteLine("1. Agregar nodo");
            Console.WriteLine("2. Agregar arista");
            Console.WriteLine("3. Mostrar grafo");
            Console.WriteLine("4. BFS");
            Console.WriteLine("5. DFS");
            Console.WriteLine("6. Salir");

            // Lee la opción seleccionada por el usuario.
            Console.Write("Seleccione una opción: ");
            int opcion = int.Parse(Console.ReadLine());

            // Realiza la operación correspondiente según la opción seleccionada.
            switch (opcion)
            {
                case 1:
                    Console.Write("Ingrese el valor del nodo: ");
                    int nodo = int.Parse(Console.ReadLine());
                    grafo.AgregarNodo(nodo);
                    break;
                case 2:
                    Console.Write("Ingrese el nodo origen: ");
                    int origen = int.Parse(Console.ReadLine());
                    Console.Write("Ingrese el nodo destino: ");
                    int destino = int.Parse(Console.ReadLine());
                    grafo.AgregarArista(origen, destino);
                    break;
                case 3:
                    grafo.MostrarGrafo();
                    break;
                case 4:
                    Console.Write("Ingrese el nodo de inicio para BFS: ");
                    int inicioBFS = int.Parse(Console.ReadLine());
                    List<int> resultadoBFS = grafo.BFS(inicioBFS);
                    Console.WriteLine($"Resultado BFS: {string.Join(", ", resultadoBFS)}");
                    break;
                case 5:
                    Console.Write("Ingrese el nodo de inicio para DFS: ");
                    int inicioDFS = int.Parse(Console.ReadLine());
                    List<int> resultadoDFS = grafo.DFS(inicioDFS);
                    Console.WriteLine($"Resultado DFS: {string.Join(", ", resultadoDFS)}");
                    break;
                case 6:
                    return;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }
    }
}
