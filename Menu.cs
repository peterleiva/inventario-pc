class Menu
{
    private List<MenuItem> _options;

    public Menu()
    {
        _options = new List<MenuItem>();
    }

    public MenuItem add(MenuItem option)
    {
        _options.Add(option);

        return option;
    }

    public void run()
    {
        int choice = 0;

        do
        {


            Console.WriteLine("\n\nMenu");

            int opt = 1;
            foreach (MenuItem item in _options)
            {
                Console.WriteLine($"{opt} - {item.Description}");
                opt++;
            }

            Console.WriteLine("0 - Sair");

            Console.Write("\nEscolha uma opção: ");
            choice = dataInput();

            if (choice - 1 >= 0 && choice - 1 < _options.Count)
            {

                _options.ElementAt(choice - 1).Action();
            }
            else
            {
                Console.WriteLine($"Por favor, escolha uma opção entre 0 e {_options.Count}");
            }

        } while (choice != 0);
    }

    public int dataInput()
    {
        try
        {
            return int.Parse(Console.ReadLine());
        }
        catch (Exception)
        {
            return -1;
        }
    }
}