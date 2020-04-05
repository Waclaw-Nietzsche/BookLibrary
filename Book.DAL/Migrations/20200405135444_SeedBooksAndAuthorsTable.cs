using Microsoft.EntityFrameworkCore.Migrations;

namespace Book.DAL.Migrations
{
    public partial class SeedBooksAndAuthorsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("INSERT INTO Authors (Name) VALUES ('Erich Maria Remarque')");
            migrationBuilder
                .Sql("INSERT INTO Authors (Name) VALUES ('Leo Tolstoy')");
            migrationBuilder
                .Sql("INSERT INTO Authors (Name) VALUES ('Friedrich Nietzsche')");
            migrationBuilder
                .Sql("INSERT INTO Authors (Name) VALUES ('Anton Chekhov')");
            
            
            migrationBuilder
                .Sql(@"INSERT INTO Books (Name, AuthorId) Values 
                    ('Three Comrades', (SELECT Id FROM Authors WHERE Name = 'Erich Maria Remarque'))");
            migrationBuilder
                .Sql(@"INSERT INTO Books (Name, AuthorId) Values 
                    ('Arch of Triumph', (SELECT Id FROM Authors WHERE Name = 'Erich Maria Remarque'))");
            migrationBuilder
                .Sql(@"INSERT INTO Books (Name, AuthorId) Values 
                    ('The Black Obelisk', (SELECT Id FROM Authors WHERE Name = 'Erich Maria Remarque'))");
            
            migrationBuilder
                .Sql(@"INSERT INTO Books (Name, AuthorId) Values 
                    ('War and Peace', (SELECT Id FROM Authors WHERE Name = 'Leo Tolstoy'))");
            migrationBuilder
                .Sql(@"INSERT INTO Books (Name, AuthorId) Values 
                    ('Anna Karenina', (SELECT Id FROM Authors WHERE Name = 'Leo Tolstoy'))");
            migrationBuilder
                .Sql(@"INSERT INTO Books (Name, AuthorId) Values 
                    ('The Death of Ivan Ilyich', (SELECT Id FROM Authors WHERE Name = 'Leo Tolstoy'))");
            
            migrationBuilder
                .Sql(@"INSERT INTO Books (Name, AuthorId) Values 
                    ('Thus Spoke Zarathustra', (SELECT Id FROM Authors WHERE Name = 'Friedrich Nietzsche'))");
            migrationBuilder
                .Sql(@"INSERT INTO Books (Name, AuthorId) Values 
                    ('Twilight of the Idols', (SELECT Id FROM Authors WHERE Name = 'Friedrich Nietzsche'))");
            migrationBuilder
                .Sql(@"INSERT INTO Books (Name, AuthorId) Values 
                    ('Ecce Homo', (SELECT Id FROM Authors WHERE Name = 'Friedrich Nietzsche'))");
            
            migrationBuilder
                .Sql(@"INSERT INTO Books (Name, AuthorId) Values 
                    ('Misery', (SELECT Id FROM Authors WHERE Name = 'Anton Chekhov'))");
            migrationBuilder
                .Sql(@"INSERT INTO Books (Name, AuthorId) Values 
                    ('The Chameleon', (SELECT Id FROM Authors WHERE Name = 'Anton Chekhov'))");
            migrationBuilder
                .Sql(@"INSERT INTO Books (Name, AuthorId) Values 
                    ('Fat and Thin ', (SELECT Id FROM Authors WHERE Name = 'Anton Chekhov'))");
            
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("DELETE FROM Books");

            migrationBuilder
                .Sql("DELETE FROM Authors");
        }
    }
}
