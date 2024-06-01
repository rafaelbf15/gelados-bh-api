using Bogus;
using GeladosBH.Api.Tests.Config;
using GeladosBH.Api.ViewModels;
using GeladosBH.API;
using GeladosBH.Domain.Tests.Config;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace GeladosBH.Api.Tests
{

    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupTests>> { }

    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {
        public readonly ApiFactory<TStartup> Factory;
        public HttpClient Client;

        public string UsuarioEmail;
        public string UsuarioSenha;

        public LoginTestsViewModel LoginTestsViewModel;

        public IntegrationTestsFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions
            {

            };

            Factory = new ApiFactory<TStartup>();
            Client = Factory.CreateClient(clientOptions);
        }

        public void GerarUserSenha()
        {
            var faker = new Faker("pt_BR");
            UsuarioEmail = faker.Internet.Email().ToLower();
            UsuarioSenha = faker.Internet.Password(8, false, "", "@1Ab_");
        }

        public void CadastrarUsuario()
        {
            GerarUserSenha();

            var registerUserViewModel = new RegisterUserViewModel()
            {
                Email = UsuarioEmail,
                Password = UsuarioSenha,
                ConfirmPassword = UsuarioSenha
            };

            var initialResponse = Client.PostAsJsonAsync("/api/v1/auth/registrar", registerUserViewModel).Result;

            initialResponse.EnsureSuccessStatusCode();
        }

        public async Task RealizarLogin()
        {
            CadastrarUsuario();

            var userData = new LoginUserViewModel
            {
                Email = UsuarioEmail,
                Password = UsuarioSenha
            };

            var response = await Client.PostAsJsonAsync("/api/v1/auth/acessar", userData);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            LoginTestsViewModel = await JsonSerializer.DeserializeAsync<LoginTestsViewModel>(stream);
        }

        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }
    }
}
