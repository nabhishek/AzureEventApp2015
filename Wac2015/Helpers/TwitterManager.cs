using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wac2015.Models;

namespace Wac2015.Helpers
{
    public class TwitterManager
    {
        private static Random _randomObject;

        public static Random RandomObject
        {
            get
            {
                if (_randomObject == null)
                    _randomObject = new Random();
                return _randomObject;
            }
            set { _randomObject = value; }
        }

        private static List<Models.TwitterAccount> _accounts;
        public static List<Models.TwitterAccount> Accounts
        {
            get
            {
                if (_accounts == null || _accounts.Count == 0)
                {
                    _accounts = new List<Models.TwitterAccount>
                    {
                        new Models.TwitterAccount("LRfm2Bhe494DnsV2t9hBogpYS",
                            "Zqj5ZcTojEpXVxiT0o6OBtrpWYtnuN2a6tNFy4Jx8sxu5YtTrT",
                            "107301638-NKLvySayHHiB9tRduZJ9i45UQwyAGMTfyExOOBBn",
                            "bmnKB2JbodiIK7TfA3fCVzNC1aOZCwHt80n8mxCBYiBEi"),
                        new Models.TwitterAccount("y81KRZjPaf01PbI74cwOwgytU",
                            "p7UDxanOZeEEKOaZH36eAl2Wtci0jZFpeOOskaeTvo7TVWlpC4",
                            "107301638-khxe6NwKWoPluyt3Iz00SUlpddlTYP4DlffZiKIr",
                            "E6OpFmjK3KxnFG7AA8KN3heRBPzNSgobce2WqA2j7OvpD"),
                        new Models.TwitterAccount("Q3VyY3wXWEb5w1ebfIQnXn0jM",
                            "j99Oo5qXAzm0lXsXXPxADpWMAAcawt4nf6azPyYi3pCUk4sySd",
                            "107301638-blHk5RAX7RrVaKvFJCkNLNkMZnpVrHqtv2lfGcO7",
                            "SOHNaWPtpmhwHopbzmrqEmpQLC3IsMipG96dzcbqRMCrA"),
                        new Models.TwitterAccount("Yc6Yq5448jigtxwUnPDmMmdUY",
                            "4wJGJ2szGf89NMA7QMIRBTdW8IkPXApupF2s58cDkutTo3ZPl4",
                            "107301638-edmCFfGQUO5KUNnWq2uH2jZcDJG04MotayeBkwZR",
                            "fxYQ3UWvY9IvFYxEoCw3Vl8kND41OkIx5P2Da5wwsySdg"),
                        new Models.TwitterAccount("SGvUR6Cy0WE6tPun1SoP7lrNR",
                            "lRPwWmGPg0dEMhGLj4rAdwXqhmYPYI58QlMLs2GKKMRN9PfJCs",
                            "107301638-7OW3hhM4Elfo1N3SFXQnEG4WjN5QAeWPa9c3vOST",
                            "ENvFH7nrR80HKoojlJDeDY4ULjbou8mIuhPIeWkMua3MU"),
                        new Models.TwitterAccount("9gDUJN3T8zmDc4Z7Czt4uNyVf",
                            "wMmINI2sJewS47Y9dcWgx5KMt2LbnGXZPKzawQoqxLQl4JpdKS",
                            "107301638-l02YnHbq9jouK7Nm316ztYynimxnO836eXzBecmY",
                            "uleqcujXAXkBWI1wW5AA7tKPeuBUCA9Jnkb2H1LmKL8AV"),
                        new Models.TwitterAccount("QWvytXUBFfRSeLBPiSuJWVNwH",
                            "lolJprDIW5AJi5I9NSMa39Rwxzroi7CZw6l6Mh1tWV9304axsd",
                            "107301638-VWCEmiVYiKHxJOyX0UIuV8OdwrXDMtwl80UDq6QF",
                            "mQOnuMUZt3o8Ts4f1zAxkWt3ATnpFk6uNFBr2iZz8D5PS"),
                        new Models.TwitterAccount("L4neaX2f5XKltj6TSk4FRxjE8",
                            "Lbox8gK85ea1yYk20neTBPajG0PtxBolppI6hlFBqVk41RukHX",
                            "107301638-q9EdkblyYOGR9rNpS9XUMASr6yBEGYIHX1Emp1D5",
                            "JVcvjYGRDGN9ujw5QI0JAqKjGpVQ5Bez4P8yo0YhRUi9l"),
                        new Models.TwitterAccount("bDEsk30RdCFQFEcI4SFtEdTnU",
                            "NOrC6Zy83M93IQRN9TK72BnXrwy1rFNJFTIRSKxyGGwSmhEZZf",
                            "107301638-htqkHuQdpjglmjsQgMLluCZ61z0f1bLs2JsDggPq",
                            "ppyXjDeJclQaOS8iNvYJAAPL0qkhcyZ4eYKNVBJAZaJXN")
                    };
                }
                return _accounts;
            }
        }
        public TwitterAccount Service
        {
            get
            {
                var index = RandomObject.Next(0, RandomObject.Next(999999));
                var index1 = RandomObject.Next(index);
                var finalIndex = index1 % Accounts.Count;
                return Accounts[finalIndex];
            }
        }


    }
}
