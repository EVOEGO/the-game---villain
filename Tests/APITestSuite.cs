using ApiTest.Models;
using ApiTest.Requests;
using ApiTest.Tests;
using Newtonsoft.Json;

namespace ApiTest;

public class APITestSuite
{
    AppAuth appAuth;
    SetupTestUser setupTestUser;
    GenTokenDetails genTokenDetails;
    VerifyTokenDetails verifyTokenDetails;
    LoggedInUserDetails loggedInUserDetails;
    UserDetails userDetails;
    NewUserResponse newUserResponse;
    NewUserResponse updateUserResponse;
    UserDetails getUserDetails;
    HealthCheckDetails healthCheckDetails;

    private string user;
    private string adduser;
    private string email;

    private int randFibNumber;

    private string[] GenTokenResponse;
    private string[] VerifyTokenResponse;
    private string[] RegisterUserResponse;
    private string[] LoginUserResponse;
    private string[] ListOfUsersResponse;
    private string[] AddNewUserResponse;
    private string[] UpdateUserResponse;
    private string[] GetUserResponse;
    private string[] HealthCheckResponse;
    private string[] LoadCpuResponse;

    private List<int> fibonacciNumbers = new List<int>{ 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181 };

    Boolean key; 

    [SetUp]
    public void setup()
    {
        setupTestUser = new SetupTestUser();
        Random rand = new Random();

        user = setupTestUser.CreateUser();
        email = setupTestUser.CreateUserEmail();
        key = true;

        adduser = rand.Next(100000, 999999).ToString();
        randFibNumber = rand.Next(0, 19);
    }

    //Creates GenToken and Asserts Response Status 200 OK
    [Test]
    public void GetGenToken()
    {
        appAuth = new AppAuth();

        GenTokenResponse = appAuth.genToken(user, email, key);

        genTokenDetails = JsonConvert.DeserializeObject<GenTokenDetails>(GenTokenResponse[0]);

        if (genTokenDetails.token.Equals(null))
        {
            Console.WriteLine("Failed with Response Code" + GenTokenResponse[1]);
            Assert.Fail();
        }
        else if (GenTokenResponse[1].Equals("OK"))
        {
            Environment.SetEnvironmentVariable("AuthToken", genTokenDetails.token);
            Assert.Pass();
        }
    }

    //Verifys the token
    [Test]
    public void VerifyToken()
    {
        appAuth = new AppAuth();

        GenTokenResponse = appAuth.genToken(user, email, key);

        genTokenDetails = JsonConvert.DeserializeObject<GenTokenDetails>(GenTokenResponse[0]);

        VerifyTokenResponse = appAuth.verifyToken(genTokenDetails.token);

        var response = JsonConvert.DeserializeObject<List<VerifyTokenDetails>>(VerifyTokenResponse[0]);

        if (VerifyTokenResponse[1].Equals("OK"))
        {
            Assert.Pass();
        }
        else
        {
            Console.WriteLine("Failed with Response Code: " + VerifyTokenResponse[1]);
            Assert.Fail();
        }
    }

    [Test]
    public void RegisterUser()
    {
        appAuth = new AppAuth();

        GenTokenResponse = appAuth.genToken(user, email, key);

        genTokenDetails = JsonConvert.DeserializeObject<GenTokenDetails>(GenTokenResponse[0]);

        VerifyTokenResponse = appAuth.verifyToken(genTokenDetails.token);

        var response = JsonConvert.DeserializeObject<List<VerifyTokenDetails>>(VerifyTokenResponse[0]);

        RegisterUserResponse = appAuth.RegisterUser(genTokenDetails.token, user, "password");

        if (RegisterUserResponse[1].Equals("OK"))
        {
            Assert.Pass();
        } else
        {
            Console.WriteLine("Failed with Response Code: " + VerifyTokenResponse[1]);
            Assert.Fail();
        }
    }

    [Test]
    public void UserLogin()
    {
        appAuth = new AppAuth();

        GenTokenResponse = appAuth.genToken(user, email, key);

        genTokenDetails = JsonConvert.DeserializeObject<GenTokenDetails>(GenTokenResponse[0]);

        VerifyTokenResponse = appAuth.verifyToken(genTokenDetails.token);

        var response = JsonConvert.DeserializeObject<List<VerifyTokenDetails>>(VerifyTokenResponse[0]);

        RegisterUserResponse = appAuth.RegisterUser(genTokenDetails.token, user, "password");

        LoginUserResponse = appAuth.UserLogin(genTokenDetails.token, user, "password");

        loggedInUserDetails = JsonConvert.DeserializeObject<LoggedInUserDetails>(LoginUserResponse[0]);

        if (LoginUserResponse[1].Equals("OK"))
        {
            Assert.Pass();
        } else
        {
            Console.WriteLine("Failed with Response Code: " + LoginUserResponse[1]);
            Assert.Fail();
        }
    }

    [Test]
    public void ListUsers()
    {
        appAuth = new AppAuth();

        GenTokenResponse = appAuth.genToken(user, email, key);

        genTokenDetails = JsonConvert.DeserializeObject<GenTokenDetails>(GenTokenResponse[0]);

        ListOfUsersResponse = appAuth.ReturnAllUsers(genTokenDetails.token);

        var AllUsers = JsonConvert.DeserializeObject<List<UserDetails>>(ListOfUsersResponse[0]);

        if (ListOfUsersResponse[1].Equals("OK"))
        {
            Assert.Pass();
        } else
        {
            Console.WriteLine("Failed with Response Code: " + ListOfUsersResponse[1]);
            Assert.Fail();
        }
    }

    [Test]
    public void AddNewUser()
    {
        appAuth = new AppAuth();

        GenTokenResponse = appAuth.genToken(user, email, key);

        genTokenDetails = JsonConvert.DeserializeObject<GenTokenDetails>(GenTokenResponse[0]);

        RegisterUserResponse = appAuth.RegisterUser(genTokenDetails.token, user, "password");

        LoginUserResponse = appAuth.UserLogin(genTokenDetails.token, user, "password");

        loggedInUserDetails = JsonConvert.DeserializeObject<LoggedInUserDetails>(LoginUserResponse[0]);

        AddNewUserResponse = appAuth.AddNewUser(loggedInUserDetails.token, adduser, 10);

        newUserResponse = JsonConvert.DeserializeObject<NewUserResponse>(AddNewUserResponse[0]);

        if (AddNewUserResponse[1].Equals("Created"))
        {
            if (newUserResponse.status.Equals("success"))
            {
                Assert.Pass();
            }
        } else
        {
            Console.WriteLine("Failed with Response Code: " + AddNewUserResponse[1]);
            Assert.Fail();
        }
    }

    [Test]
    public void UpdateUser()
    {
        appAuth = new AppAuth();

        GenTokenResponse = appAuth.genToken(user, email, key);

        genTokenDetails = JsonConvert.DeserializeObject<GenTokenDetails>(GenTokenResponse[0]);

        RegisterUserResponse = appAuth.RegisterUser(genTokenDetails.token, user, "password");

        LoginUserResponse = appAuth.UserLogin(genTokenDetails.token, user, "password");

        loggedInUserDetails = JsonConvert.DeserializeObject<LoggedInUserDetails>(LoginUserResponse[0]);

        AddNewUserResponse = appAuth.AddNewUser(loggedInUserDetails.token, adduser, 10);

        newUserResponse = JsonConvert.DeserializeObject<NewUserResponse>(AddNewUserResponse[0]);

        UpdateUserResponse = appAuth.UpdateUser(loggedInUserDetails.token, adduser, 5);

        updateUserResponse = JsonConvert.DeserializeObject<NewUserResponse>(UpdateUserResponse[0]);

        if (UpdateUserResponse[1].Equals("NoContent"))
        {
            Assert.Pass();
        }
        else
        {
            Console.WriteLine("Failed with Response Code: " + UpdateUserResponse[1]);
            Assert.Fail();
        }
    }

    [Test]
    public void DeleteUser()
    {
        //Didn't understand what delete-key was so left this one out
    }

    [Test]
    public void GetUserByUsername()
    {
        appAuth = new AppAuth();

        GenTokenResponse = appAuth.genToken(user, email, key);

        genTokenDetails = JsonConvert.DeserializeObject<GenTokenDetails>(GenTokenResponse[0]);

        RegisterUserResponse = appAuth.RegisterUser(genTokenDetails.token, user, "password");

        LoginUserResponse = appAuth.UserLogin(genTokenDetails.token, user, "password");

        loggedInUserDetails = JsonConvert.DeserializeObject<LoggedInUserDetails>(LoginUserResponse[0]);

        AddNewUserResponse = appAuth.AddNewUser(loggedInUserDetails.token, adduser, 10);

        newUserResponse = JsonConvert.DeserializeObject<NewUserResponse>(AddNewUserResponse[0]);

        GetUserResponse = appAuth.GetUserByUsername(loggedInUserDetails.token, adduser);

        var details = JsonConvert.DeserializeObject<List<UserDetails>>(GetUserResponse[0]);

        if (GetUserResponse[1].Equals("OK"))
        {
            Assert.Pass();
        }
        else
        {
            Console.WriteLine("Failed with Response Code: " + GetUserResponse[1]);
            Assert.Fail();
        }
    }

    [Test]
    public void TestHealthCheck()
    {
        appAuth = new AppAuth();

        GenTokenResponse = appAuth.genToken(user, email, key);

        genTokenDetails = JsonConvert.DeserializeObject<GenTokenDetails>(GenTokenResponse[0]);

        RegisterUserResponse = appAuth.RegisterUser(genTokenDetails.token, user, "password");

        LoginUserResponse = appAuth.UserLogin(genTokenDetails.token, user, "password");

        loggedInUserDetails = JsonConvert.DeserializeObject<LoggedInUserDetails>(LoginUserResponse[0]);

        HealthCheckResponse = appAuth.HealthCheck(loggedInUserDetails.token);

        healthCheckDetails = JsonConvert.DeserializeObject<HealthCheckDetails>(HealthCheckResponse[0]);

        if (HealthCheckResponse[1].Equals("OK"))
        {
            Assert.Pass();
        } else
        {
            Console.WriteLine("Failed with Response Code: " + HealthCheckResponse[1]);
            Assert.Fail();
        }
    }

    [Test]
    public void VerifyHealthCheckModelDetails()
    {
        appAuth = new AppAuth();

        GenTokenResponse = appAuth.genToken(user, email, key);

        genTokenDetails = JsonConvert.DeserializeObject<GenTokenDetails>(GenTokenResponse[0]);

        RegisterUserResponse = appAuth.RegisterUser(genTokenDetails.token, user, "password");

        LoginUserResponse = appAuth.UserLogin(genTokenDetails.token, user, "password");

        loggedInUserDetails = JsonConvert.DeserializeObject<LoggedInUserDetails>(LoginUserResponse[0]);

        HealthCheckResponse = appAuth.HealthCheck(loggedInUserDetails.token);

        healthCheckDetails = JsonConvert.DeserializeObject<HealthCheckDetails>(HealthCheckResponse[0]);

        if (HealthCheckResponse[1].Equals("OK"))
        {
            string expectedModel = "Intel(R) Xeon(R) Platinum 8375C CPU @ 2.90GHz";
            Boolean result = true;

            for (int x = 0; x < healthCheckDetails.cpu.Count; x++)
            {
                if (healthCheckDetails.cpu[x].model.Equals(expectedModel))
                {
                    result = true;
                } else
                {
                    result = false;
                }
            }

            if (result.Equals(true))
            {
                Assert.Pass();
            }
            else
            {
                Console.WriteLine("Health Check Cpu Model different to expected model");
                Assert.Fail();
            }
        }
        else
        {
            Console.WriteLine("Failed with Response Code: " + HealthCheckResponse[1]);
            Assert.Fail();
        }
    }

    [Test]
    public void LoadCpu()
    {
        appAuth = new AppAuth();

        GenTokenResponse = appAuth.genToken(user, email, key);

        genTokenDetails = JsonConvert.DeserializeObject<GenTokenDetails>(GenTokenResponse[0]);

        RegisterUserResponse = appAuth.RegisterUser(genTokenDetails.token, user, "password");

        LoginUserResponse = appAuth.UserLogin(genTokenDetails.token, user, "password");

        loggedInUserDetails = JsonConvert.DeserializeObject<LoggedInUserDetails>(LoginUserResponse[0]);

        LoadCpuResponse = appAuth.LoadCpu(loggedInUserDetails.token, randFibNumber);

        if (LoadCpuResponse[1].Equals("OK"))
        {
            Assert.Pass();
        } else
        {
            Console.WriteLine("Failed with Response Code: " + LoadCpuResponse[1]);
            Assert.Fail();
        }
    }

    [Test]
    public void TestFibonacciSequence()
    {
        appAuth = new AppAuth();

        GenTokenResponse = appAuth.genToken(user, email, key);

        genTokenDetails = JsonConvert.DeserializeObject<GenTokenDetails>(GenTokenResponse[0]);

        RegisterUserResponse = appAuth.RegisterUser(genTokenDetails.token, user, "password");

        LoginUserResponse = appAuth.UserLogin(genTokenDetails.token, user, "password");

        loggedInUserDetails = JsonConvert.DeserializeObject<LoggedInUserDetails>(LoginUserResponse[0]);

        LoadCpuResponse = appAuth.LoadCpu(loggedInUserDetails.token, randFibNumber);

        if (LoadCpuResponse[1].Equals("OK"))
        {
            if (int.Parse(LoadCpuResponse[0]).Equals(fibonacciNumbers[randFibNumber]))
            {
                Assert.Pass();
            }
            else
            {
                Console.WriteLine("Expected: " + fibonacciNumbers[randFibNumber]);
                Console.WriteLine("Response: " + LoadCpuResponse[0]);
                Assert.Fail();
            }
        }
        else
        {
            Console.WriteLine("Failed with Response Code: " + LoadCpuResponse[1]);
            Assert.Fail();
        }
    }
}
