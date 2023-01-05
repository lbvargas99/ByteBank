using ByteBank.Entities;

namespace Program
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<Usuario> users = new List<Usuario>();
            int opcao;
            do
            {
                ShowMenu();
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        Usuario user = CreateUser();
                        users.Add(user);
                        break;
                    case 2:
                        ListUsers(users);
                        break;
                    case 3:
                        DeleteUser(users);
                        break;
                    case 4:
                        ShowDetailsAccount(users);
                        break;
                    case 5:
                        ShowTotalBullet(users);
                        break;
                    case 6:
                        ManipulateAccount(users);
                        break;
                    default: break;
                }

            } while (opcao != 0);

        }

        private static void ManipulateAccount(List<Usuario> users)
        {
            System.Console.Write("Informe o CPF da conta que deseja manipular: ");
            string cpf = Console.ReadLine();

            if (users.Exists(user => user.Cpf == cpf))
            {
                System.Console.WriteLine("deu certo");
            }
        }

        private static void ShowTotalBullet(List<Usuario> users)
        {
            System.Console.WriteLine("Saldo total armazenado no banco:");
            System.Console.WriteLine($"R$: {users.Sum(bullet => bullet.Saldo)}");
        }

        private static void ShowDetailsAccount(List<Usuario> users)
        {
            System.Console.Write("Informe o CPF do usuário que deseja visualizar: ");
            string cpf = Console.ReadLine();
            Usuario user = users.FirstOrDefault(user => user.Cpf == cpf);

            System.Console.WriteLine($":: Detalhes da conta {user.Nome} ::");
            System.Console.WriteLine($"Nome:   {user.Nome}");
            System.Console.WriteLine($"CPF:    {user.Cpf}");
            System.Console.WriteLine($"Saldo:  {user.Saldo}");
        }

        private static void DeleteUser(List<Usuario> users)
        {
            System.Console.Write("Informe o CPF do usuário a ser removido: ");
            string cpf = Console.ReadLine();

            Usuario contaRemover = users.FirstOrDefault(user => user.Cpf == cpf);
            users.Remove(contaRemover);
        }

        private static void ListUsers(List<Usuario> users)
        {
            foreach (var user in users)
            {
                System.Console.WriteLine("---");
                System.Console.WriteLine($"Nome:  {user.Nome}");
                System.Console.WriteLine($"CPF:   {user.Cpf}");
                System.Console.WriteLine($"Saldo: {user.Saldo}");
                System.Console.WriteLine("---");
            }
        }

        private static Usuario CreateUser()
        {
            System.Console.Write("Informe seu nome: ");
            string nome = Console.ReadLine();
            System.Console.Write("Informe seu cpf: ");
            string cpf = Console.ReadLine();
            System.Console.Write("Informe sua senha: ");
            string password = Console.ReadLine();
            System.Console.Write("Informe o saldo da conta: ");
            double saldo = double.Parse(Console.ReadLine());

            return new Usuario(nome, cpf, password, saldo);
        }

        private static void ShowMenu()
        {
            System.Console.WriteLine("..:: Bem vindo ao ByteBank ::..");
            System.Console.WriteLine("[1] - Inserir novo usuário");
            System.Console.WriteLine("[2] - Listar usuários registrados");
            System.Console.WriteLine("[3] - Deletar usuário");
            System.Console.WriteLine("[4] - Detalhes de um usuário");
            System.Console.WriteLine("[5] - Total armazenado no banco");
            System.Console.WriteLine("[6] - Manipular conta");
            System.Console.WriteLine("[0] - Sair");
            System.Console.Write("Escolha a opção desejada: ");
        }
    }
}