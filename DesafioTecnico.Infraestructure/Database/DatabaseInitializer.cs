﻿using Dapper;
using System.Data;

namespace DesafioTecnico.Infraestructure.Database
{
    public static class DatabaseInitializer
    {
        public static void Initialize(IDbConnection connection)
        {
            SQLitePCL.Batteries.Init();

            CreateTables(connection);
            SeedData(connection);
        }

        private static void CreateTables(IDbConnection connection)
        {
            var createTables = @"
                CREATE TABLE IF NOT EXISTS contacorrente (
                    idcontacorrente TEXT(37) PRIMARY KEY,
                    numero INTEGER(10) NOT NULL UNIQUE,
                    nome TEXT(100) NOT NULL,
                    ativo INTEGER(1) NOT NULL default 0,
                    CHECK (ativo in (0,1))
                );

                CREATE TABLE IF NOT EXISTS movimento (
                    idmovimento TEXT(37) PRIMARY KEY,
                    idcontacorrente TEXT(37) NOT NULL,
                    datamovimento TEXT(25) NOT NULL,
                    tipomovimento TEXT(1) NOT NULL,
                    valor REAL NOT NULL,
                    CHECK (tipomovimento in ('C','D')),
                    FOREIGN KEY(idcontacorrente) REFERENCES contacorrente(idcontacorrente)
                );";

            connection.Execute(createTables);
        }

        private static void SeedData(IDbConnection connection)
        {
            var countContas = connection.QueryFirstOrDefault<int>("SELECT COUNT(*) FROM contacorrente");
            if (countContas == 0)
            {
                var insertContas = @"
                    INSERT INTO contacorrente(idcontacorrente, numero, nome, ativo) VALUES('B6BAFC09-6967-ED11-A567-055DFA4A16C9', 123, 'Katherine Sanchez', 1);
                    INSERT INTO contacorrente(idcontacorrente, numero, nome, ativo) VALUES('FA99D033-7067-ED11-96C6-7C5DFA4A16C9', 456, 'Eva Woodward', 1);
                    INSERT INTO contacorrente(idcontacorrente, numero, nome, ativo) VALUES('382D323D-7067-ED11-8866-7D5DFA4A16C9', 789, 'Tevin Mcconnell', 1);
                    INSERT INTO contacorrente(idcontacorrente, numero, nome, ativo) VALUES('F475F943-7067-ED11-A06B-7E5DFA4A16C9', 741, 'Ameena Lynn', 0);
                    INSERT INTO contacorrente(idcontacorrente, numero, nome, ativo) VALUES('BCDACA4A-7067-ED11-AF81-825DFA4A16C9', 852, 'Jarrad Mckee', 0);
                    INSERT INTO contacorrente(idcontacorrente, numero, nome, ativo) VALUES('D2E02051-7067-ED11-94C0-835DFA4A16C9', 963, 'Elisha Simons', 0);";

                connection.Execute(insertContas);
            }
        }
    }
}
