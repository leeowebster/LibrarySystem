using Xunit;
using Moq;
using System;
using System.Threading.Tasks;
using LibrarySystem.Application.DTOs;
using LibrarySystem.Application.Services;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Domain.Models;

namespace LibrarySystem.Test;

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
    public void Borrow_SuccesfullBorrow()
    {
        // 1. ARRANGE (Preparação)
        var borrowDto = new BorrowDTO { BookId = 99, PersonId = 100 };

        var availablebook = new Books { Id = 99, IsAvailable = true, Title = "The Book" };
        var existingPerson = new People { Id = 100, Name = "Leonardo" };


        _mockBookRepo
            .Setup(r => r.GetBookById(99))
            .Returns(availablebook); 


        _mockPeopleRepo
            .Setup(r => r.GetById(100))
            .Returns(existingPerson);

        // 2. ACT (Ação)
        _borrowService.Borrow(borrowDto);

        // 3. ASSERT
        _mockBorrowRepo.Verify(r => r.Borrow(It.IsAny<Books>(), It.IsAny<People>()), Times.Once);
    }

    [Fact]
    public void Borrow_WhenPersonHasMoreThanThreeBooks_ShouldThrowInvalidOperationException()
    {
        // 1. ARRANGE
        var borrowDto = new BorrowDTO { BookId = 300, PersonId = 100 };
        var book = new Books { Id = 300, IsAvailable = true, Title = "Harry Potter 1" };
        var existingPerson = new People { Id = 100, Name = "Leonardo" };

        _mockBookRepo
            .Setup(r => r.GetBookById(300))
            .Returns(book);
        _mockPeopleRepo
            .Setup(r => r.GetById(100))
            .Returns(existingPerson);
        _mockBorrowRepo
            .Setup(r => r.GetBorrowCount(100))
            .Returns(3);

        // 2. ACT (Ação)
        var actual = Assert.Throws<InvalidOperationException>(() => _borrowService.Borrow(borrowDto));

        // 3. ASSERT
        var expected = "Person has 3 books. Return one before borrowing another.";
        Assert.Equal(expected, actual.Message);
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
            .Setup(r => r.GetBookById(99))
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
        Assert.Equal("Book is not available.", exception.Message);

        // Racional: Verificar que o método de persistência (BorrowAsync) NUNCA foi chamado.
        // Se a regra de negócio for violada, o código não deve tocar no repositório de persistência.
        _mockBorrowRepo.Verify(r => r.Borrow(It.IsAny<Books>(), It.IsAny<People>()), Times.Never);
    }

    [Fact]
    public void Borrow_WhenBookDoesNotExist_ShouldThrowInvalidOperation()
    {
        var borrowDto = new BorrowDTO { BookId = 90, PersonId = 100 };

        
        var existingPerson = new People { Id = 100, Name = "João" };

        _mockPeopleRepo
            .Setup(r => r.GetById(100))
            .Returns(existingPerson);

        var exception = Assert.Throws<InvalidOperationException>(() => _borrowService.Borrow(borrowDto));

        Assert.Equal("Book does not exist.", exception.Message);

        _mockBorrowRepo.Verify(r => r.Borrow(It.IsAny<Books>(), It.IsAny<People>()), Times.Never);

    }

    [Fact]
    public void Borrow_WhenPersonDoesNotExist_ShouldThrowInvalidOperation()
    {
        var borrowDto = new BorrowDTO { BookId = 100, PersonId = 90 };
        var existingBook = new Books { Id = 100, Author = "Leonardo Webster"};
        _mockBookRepo
            .Setup(r => r.GetBookById(100))
            .Returns(existingBook);

        var exception = Assert.Throws<InvalidOperationException>(() => _borrowService.Borrow(borrowDto));

        Assert.Equal("Person does not exist.", exception.Message);
        _mockBorrowRepo.Verify(r => r.Borrow(It.IsAny<Books>(), It.IsAny<People>()), Times.Never);

    }

    [Fact]
    public void ReturnBook_WhenBorrowDoesNotExist_ShouldThrowInvalidExpection()
    {
        var wrongBorrowID = new BorrowID();
        var actual = Assert.Throws<InvalidOperationException>(() => _borrowService.ReturnBook(wrongBorrowID));

        var expected = "Borrow record does not exist.";

        Assert.Equal(expected, actual.Message);
    }

    [Fact]
    public void ReturnBook_WhenBookHasAlreadyBeenReturned_ShouldThrowInvalidExpection()
    {
        var borrowId = new BorrowID() { Id=100};

        var newBorrow = new Borrow() {  
            Id =100, 
            BookId = 22, 
            DateBorrowed = DateTime.UtcNow, 
            DateReturned = DateTime.Now, 
            PeopleID = 90};

        _mockBorrowRepo.
            Setup(r => r.GetBorrowById(100))
            .Returns(newBorrow);

        var actual = Assert.Throws<InvalidOperationException>(() => _borrowService.ReturnBook(borrowId));

        var expected = "This book has already been returned.";
        Assert.Equal(expected, actual.Message);
    }

    [Fact]

    public void ReturnBook_ShouldReturnSucessfully()
    {
        //Arrange
        var borrowId = new BorrowID() { Id = 100 };

        var newBorrow = new Borrow()
        {
            Id = 100,
            BookId = 22,
            DateBorrowed = DateTime.UtcNow,
            PeopleID = 90
        };

        _mockBorrowRepo.
            Setup(r => r.GetBorrowById(100))
            .Returns(newBorrow);

        //Act
        _borrowService.ReturnBook(borrowId);
       
        //Assert
        _mockBorrowRepo.Verify(r => r.Return(It.IsAny<Borrow>(), It.IsAny<Books>()), Times.Once);
    }




}
