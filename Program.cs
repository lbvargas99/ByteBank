using ByteBank.Entities;

namespace Program
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Clear();
            List<Usuario> users = new List<Usuario>();
            int opcao;
            do
            {
                ShowMenu();
                opcao = int.Parse(Console.ReadLine());
                switch (opcao)
                {
                    case 1:
                        Console.Clear();
                        Usuario user = CreateUser(users);
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
                        SleepToRedirect();
                        System.Console.Write("Informe seu CPF: ");
                        ManipulateAccount(users);
                        break;
                    default: break;
                }

            } while (opcao != 0);

        }

        private static void SleepToRedirect()
        {
            System.Console.WriteLine("Redirecionando...");
            Thread.Sleep(1000);
            Console.Clear();
        }

        private static void ManipulateAccount(List<Usuario> users)
        {
            string cpf = Console.ReadLine();
            Usuario currentUser = new Usuario();
            do
            {
                if (!users.Exists(user => user.Cpf == cpf))
                {
                    System.Console.WriteLine("Por favor, informe um CPF válido");
                    System.Console.WriteLine("[1] - Login");
                    System.Console.WriteLine("[2] - Cadastro");
                    System.Console.Write("Informe a opção desejada: ");
                    int newOption = int.Parse(Console.ReadLine());
                    switch (newOption)
                    {
                        case 1:
                            if (!ExistAnyAccount(users))
                            {
                                return;
                            }
                            System.Console.Write("Informe seu CPF: ");
                            cpf = Console.ReadLine();
                            if (users.Exists(user => user.Cpf == cpf))
                            {
                                System.Console.Write("Informe sua senha: ");
                                string password = Console.ReadLine();
                                if (users.Exists(user => user.Password == password))
                                {
                                    currentUser = users.FirstOrDefault(user => user.Cpf == cpf);
                                }
                                else
                                {
                                    System.Console.WriteLine("Senha incorreta, retornando ao menu principal");
                                }
                            }
                            break;

                        case 2:
                            Usuario user = CreateUser(users);
                            users.Add(user);
                            break;

                        default: System.Console.WriteLine("Opção inválida"); break;
                    }
                }

                currentUser = users.FirstOrDefault(user => user.Cpf == cpf);
                ManipulateMenu();
                int option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        ToDeposite(currentUser, users);
                        SleepToRedirect();
                        break;

                    case 2:
                        Sacar(currentUser, users);
                        SleepToRedirect();
                        break;

                    case 3:
                        Transferir(currentUser, users);
                        SleepToRedirect();
                        break;

                    default:
                        System.Console.WriteLine("Opção não encontrada");
                        SleepToRedirect();
                        break;
                }
                if (option == 0) break;
            } while (users.Exists(user => user.Cpf == cpf));
            System.Console.WriteLine("Retornando ao menu principal...");
            SleepToRedirect();
        }

        private static void Transferir(Usuario currentUser, List<Usuario> users)
        {
            System.Console.Write("Informe o CPF do destinatário: ");
            string cpfDestinatario = Console.ReadLine();
            if (!users.Exists(user => user.Cpf == cpfDestinatario))
            {
                System.Console.WriteLine("Conta não encontrada");
                return;
            }
            Usuario userDestinatario = users.FirstOrDefault(user => user.Cpf == cpfDestinatario);

            System.Console.Write("Informe a quantia que deseja transferir: ");
            double value = double.Parse(Console.ReadLine());
            if (value > currentUser.Saldo)
            {
                System.Console.WriteLine("Não é possível transferir valor maior do que você possuí!");
                return;
            }
            userDestinatario.Saldo += value;
            currentUser.Saldo -= value;

            users.Remove(userDestinatario);
            users.Remove(currentUser);
            users.Add(userDestinatario);
            users.Add(currentUser);
        }

        private static void Sacar(Usuario currentUser, List<Usuario> users)
        {
            System.Console.Write("Informe o valor que deseja sacar: ");
            double saque = double.Parse(Console.ReadLine());
            if (saque > currentUser.Saldo)
            {
                System.Console.WriteLine("Não é possível sacar um valor maior que seu saldo");
                return;
            }
            currentUser.Saldo -= saque;
            users.Remove(currentUser);
            users.Add(currentUser);
        }

        private static void ToDeposite(Usuario currentUser, List<Usuario> users)
        {
            System.Console.Write("Informe o valor a ser depositado: ");
            double newSaldo = double.Parse(Console.ReadLine());
            if (newSaldo > 0)
            {
                currentUser.Saldo += newSaldo;
                users.Remove(currentUser);
                users.Add(currentUser);
                return;
            }
            System.Console.WriteLine("Não é possivel depositar o valor 0 ou negativo");
        }

        private static void ManipulateMenu()
        {

            System.Console.WriteLine("[1] - Depositar");
            System.Console.WriteLine("[2] - Sacar");
            System.Console.WriteLine("[3] - Transferência");
            System.Console.WriteLine("[0] - Retornar ao menu principal");
            System.Console.Write("Informe a opção desejada: ");
        }

        private static void ShowTotalBullet(List<Usuario> users)
        {
            System.Console.WriteLine("Saldo total armazenado no banco:");
            System.Console.WriteLine($"R$: {users.Sum(bullet => bullet.Saldo)}");
            System.Console.WriteLine("Redirecionando...");
            Thread.Sleep(3000);
        }

        private static void ShowDetailsAccount(List<Usuario> users)
        {
            if (!ExistAnyAccount(users))
            {
                return;
            }
            System.Console.Write("Informe o CPF do usuário que deseja visualizar: ");
            string cpf = Console.ReadLine();
            Usuario user = users.FirstOrDefault(user => user.Cpf == cpf);

            System.Console.WriteLine($":: Detalhes da conta {user.Nome} ::");
            System.Console.WriteLine($"Nome:   {user.Nome}");
            System.Console.WriteLine($"CPF:    {user.Cpf}");
            System.Console.WriteLine($"Saldo:  {user.Saldo}");
            System.Console.WriteLine("Redirecionando...");
            Thread.Sleep(3000);
        }

        private static void DeleteUser(List<Usuario> users)
        {
            SleepToRedirect();
            System.Console.WriteLine(":: Deletar usuário");
            if (!ExistAnyAccount(users))
            {
                return;
            }
            System.Console.Write("Informe o CPF do usuário a ser removido: ");
            string cpf = Console.ReadLine();
            Thread.Sleep(500);
            System.Console.WriteLine("Conta removida");

            Usuario contaRemover = users.FirstOrDefault(user => user.Cpf == cpf);
            users.Remove(contaRemover);
        }

        private static void ListUsers(List<Usuario> users)
        {
            SleepToRedirect();
            System.Console.WriteLine(":: Lista de contas");
            if (!ExistAnyAccount(users))
            {
                return;
            }
            foreach (var user in users)
            {
                System.Console.WriteLine("---");
                System.Console.WriteLine($"Nome:  {user.Nome}");
                System.Console.WriteLine($"CPF:   {user.Cpf}");
                System.Console.WriteLine($"Saldo: {user.Saldo}");
                System.Console.WriteLine("---");
            }
            System.Console.WriteLine("Redirecionando...");
            Thread.Sleep(5000);
        }

        private static Usuario CreateUser(List<Usuario> users)
        {
            SleepToRedirect();
            System.Console.WriteLine(":: Cadastro de conta");
            bool flag = false;
            string nome;
            string cpf;
            string password;
            double saldo;
            do
            {
                System.Console.Write("Informe seu nome: ");
                nome = Console.ReadLine();
                System.Console.Write("Informe seu cpf: ");
                cpf = Console.ReadLine();
                if (users.Exists(user => user.Cpf == cpf))
                {
                    System.Console.WriteLine("Este CPF já está cadastrado, reiniciando cadastro");
                    return CreateUser(users);
                    break;
                }
                System.Console.Write("Informe sua senha: ");
                password = HidePassword();
                System.Console.Write("Informe o saldo da conta: ");
                saldo = double.Parse(Console.ReadLine());
                flag = true;
            } while (flag == false);

            SleepToRedirect();
            return new Usuario(nome, cpf, password, saldo);
        }

        private static string HidePassword()
        {
            string password = "";
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
            }
            Console.Write("\n");
            return password;
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

        private static bool ExistAnyAccount(List<Usuario> users)
        {
            if (users.Count == 0)
            {
                SleepToRedirect();
                System.Console.WriteLine("Não existe conta registrada!");
                return false;
            }
            SleepToRedirect();
            return true;
        }
    }
}