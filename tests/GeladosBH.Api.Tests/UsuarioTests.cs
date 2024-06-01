using GeladosBH.Api.ViewModels;
using GeladosBH.API;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GeladosBH.Api.Tests
{
    [TestCaseOrderer("GeladosBH.Api.Tests.PriorityOrderer", "GeladosBH.Api.Tests")]
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class UsuarioTests
    {
        private readonly IntegrationTestsFixture<StartupTests> _testsFixture;

        public UsuarioTests(IntegrationTestsFixture<StartupTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Realizar cadastro de usuário deve retornar sucesso"), TestPriority(1)]
        [Trait("Categoria", "Integração Api - Usuario")]
        public async Task Usuario_RealizarCadastro_DeveExecutarComSucesso()
        {
            // Arrange
            _testsFixture.GerarUserSenha();
            var registerUserViewModel = new RegisterUserViewModel()
            {
                Email = _testsFixture.UsuarioEmail,
                Password = _testsFixture.UsuarioSenha,
                ConfirmPassword = _testsFixture.UsuarioSenha
            };

            // Act
            var initialResponse = await _testsFixture.Client.PostAsJsonAsync("/api/v1/auth/registrar", registerUserViewModel);

            // Assert
            initialResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Realizar Login deve retornar sucesso"), TestPriority(2)]
        [Trait("Categoria", "Integração Api - Usuario")]
        public async Task Login_RealizarLogin_DeveExecutarComSucesso()
        {
            // Arrange
            var userData = new LoginUserViewModel
            {
                Email = _testsFixture.UsuarioEmail,
                Password = _testsFixture.UsuarioSenha
            };

            // Act
            var response = await _testsFixture.Client.PostAsJsonAsync("/api/v1/auth/acessar", userData);

            //Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
