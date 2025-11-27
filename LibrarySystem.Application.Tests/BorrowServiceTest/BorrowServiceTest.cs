using Xunit;
using Moq;
using System;
using System.Threading.Tasks;
using LibrarySystem.Application.DTOs;
using LibrarySystem.Application.Services;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Domain.Models;

namespace BorrowServiceTest;

public class BorrowServiceTest
{
    private readonly Mock<IBookRepository> _mockBookRepo;
    private readonly Mock<IPeopleRepository> _mockPeopleRepo;
    private readonly Mock<IBorrowRepository> _mockBorrowRepo;

    private readonly BorrowService _borrowService;

    public BorrowServiceTest()
    {
        _mockBookRepo = new Mock<IBookRepository>();
        _mockPeopleRepo = new Mock<IPeopleRepository>();
        _mockBorrowRepo = new Mock<IBorrowRepository>();

        _borrowService = new BorrowService(_mockBookRepo.Object, _mockPeopleRepo.Object, _mockBorrowRepo.Object);
    }



    [Fact]
    public void Borrow_WhenBookIsNotAvailable_ShouldThrowInvalidOperationException()
    {
        // 1. ARRANGE (Preparação)
        // Racional: Criar o DTO de entrada para a chamada do serviço.
        var borrowDto = new BorrowDTO { BookId = 99, PersonId = 100 };

        // Racional: Criar os Models de Domínio que o Repositório "retornaria".
        // O livro deve existir, mas NÃO ESTAR DISPONÍVEL.
        var unavailableBook = new Books { Id = 99, IsAvailable = false, Title = "O Livro Indisponível" };
        var existingPerson = new People { Id = 100, Name = "João" };

        // Racional: Configurar o comportamento do Mock (Moq.Setup).
        // Diz ao Mock: "Quando alguém chamar GetByIdAsync(99), retorne o livro indisponível."
        _mockBookRepo
            .Setup(r => r.GetById(99))
            .Returns(unavailableBook); // Retorna o livro indisponível

        // Diz ao Mock: "Quando alguém chamar GetByIdAsync(100), retorne a pessoa."
        _mockPeopleRepo
            .Setup(r => r.GetById(100))
            .Returns(existingPerson);

        // 2. ACT (Ação)
        // Racional: Usar Assert.ThrowsAsync para verificar se o código lança a exceção esperada.
        var exception = Assert.Throws<InvalidOperationException>(() => _borrowService.Borrow(borrowDto));

        // 3. ASSERT (Verificação)
        // Racional: Verificar se a lógica de negócio foi executada corretamente.
        Assert.Equal("Book is not available or person does not exist.", exception.Message);

        // Racional: Verificar que o método de persistência (BorrowAsync) NUNCA foi chamado.
        // Se a regra de negócio for violada, o código não deve tocar no repositório de persistência.
        _mockBorrowRepo.Verify(r => r.Borrow(It.IsAny<Books>(), It.IsAny<People>()), Times.Never);
    }
}
