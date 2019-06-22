using System;
using System.Data;
using MySql.Data.MySqlClient;
using AplicacaoFaculdade.Models;

namespace AplicacaoFaculdade.DatabaseContext {

    public class DocumentoContext {

        private MySqlConnection database;
        private MySqlCommand mySqlCommand;
        private MySqlDataReader mySqlDataReader;
        private MySqlDataAdapter mySqlDataAdapter;

        public DocumentoContext() {
            database = Database.GetInstance().GetConnection();
        }

        public DataTable GetDocumentos(Pessoa pessoa) {
            DataTable documentosDataTable = new DataTable();
            mySqlCommand = new MySqlCommand("SELECT * FROM  PessoasDocumentos INNER JOIN Pessoas ON fkDocumento = documentoId INNER JOIN Pessoas on fkPessoa = pessoaId WHERE pessoaId = @PessoaId", database);
            mySqlCommand.Parameters.AddWithValue("@PessoaId", pessoa.Id);
            mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            using (mySqlDataAdapter) {
                mySqlDataAdapter.Fill(documentosDataTable);
                return documentosDataTable;
            }
        }

        public Documento GetDocumento(Documento documento) {
            mySqlCommand = new MySqlCommand("SELECT documentoId, documentoNumero, documentoFkTipo, documentoStatus FROM PessoasDocumentos INNER JOIN Documentos on fkDocumento = documentoId WHERE documentoId = @DocumentoId", database);
            mySqlCommand.Parameters.AddWithValue("@DocumentoId", documento.Id);
            using (mySqlCommand) {
                mySqlDataReader = mySqlCommand.ExecuteReader();
                using (mySqlDataReader) {
                    if (mySqlDataReader.HasRows) {
                        return new Documento() {
                            Id = mySqlDataReader.GetInt32(0),
                            Numero = mySqlDataReader.GetString(1),
                            Tipo = mySqlDataReader.GetInt32(2),
                            Status = mySqlDataReader.GetBoolean(3)
                        };
                    }
                }
            }
            return new Documento();
        }

        public bool? CreateDocumento(Documento documento, Pessoa pessoa) {
            string query = "INSERT INTO Documentos() VALUES (null, @DocumentoTipo, @DocumentoNumero, true)";
            mySqlCommand = new MySqlCommand(query, database);
            mySqlCommand.Parameters.AddWithValue("@DocumentoTipo", documento.Tipo);
            mySqlCommand.Parameters.AddWithValue("@DocumentoNumero", documento.Numero);

            using (mySqlCommand) {
                if (mySqlCommand.ExecuteNonQuery() > 0) {
                    int newDocumentoId = (int) mySqlCommand.LastInsertedId;
                    query = "INSERT INTO PessoasDcoumentos() VALUES (@PessoaId, @DocumentoId)";
                    mySqlCommand = new MySqlCommand(query, database);
                    mySqlCommand.Parameters.AddWithValue("@PessoaId", pessoa.Id);
                    mySqlCommand.Parameters.AddWithValue("@DocumentoId", newDocumentoId);
                    return (mySqlCommand.ExecuteNonQuery() > 0);
                }
            }

            return false;
        }

        public DataTable GetDocumentoTipos(bool tipoDocumentoStatus = true) {
            DataTable documentoTiposDatatable = new DataTable();
            string query = "SELECT * FROM TiposDocumento WHERE tipoDocumentoStatus = @tipoDocumentoStatus";
            mySqlCommand = new MySqlCommand(query, database);
            mySqlCommand.Parameters.AddWithValue("@tipoDocumentoStatus", tipoDocumentoStatus);
            mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            using (mySqlDataAdapter) {
                mySqlDataAdapter.Fill(documentoTiposDatatable);
                return documentoTiposDatatable;
            }
        }

        public DocumentoTipo GetDocumentoTipos(DocumentoTipo documentoTipo) {
            mySqlCommand = new MySqlCommand("SELECT * FROM TiposDcoumento WHERE tipoDocumentoId = @TipoDocumentoId", database);
            mySqlCommand.Parameters.AddWithValue("@TipoDocumentoId", documentoTipo.Id.Value);
            using (mySqlCommand) {
                mySqlDataReader = mySqlCommand.ExecuteReader();
                if (mySqlDataReader.Read()) {
                    if (mySqlDataReader.HasRows) {
                        return new DocumentoTipo() {
                            Id = mySqlDataReader.GetInt32(0),
                            Nome = mySqlDataReader.GetString(1),
                            Status = mySqlDataReader.GetBoolean(2)
                        };
                    }
                }
            }
            return new DocumentoTipo();
        }

        public bool UpdateDocumento(Documento documento) {
            string query = "UPDATE Documentos SET documentoFkTipo = @DocumentoTipo, documentoNumero = @DocumentoNumero, documentoStatus = @DocumentoStatus WHERE documentoId = @DocumentoId";
            mySqlCommand = new MySqlCommand(query, database);
            mySqlCommand.Parameters.AddWithValue("@DocumentoTipo", documento.Tipo);
            mySqlCommand.Parameters.AddWithValue("@DocumentoNumero", documento.Numero);
            mySqlCommand.Parameters.AddWithValue("@DocumentoStatus", documento.Status);
            mySqlCommand.Parameters.AddWithValue("@DocumentoId", documento.Id);
            using (mySqlCommand) {
                return (mySqlCommand.ExecuteNonQuery() > 0);
            }
        }

        public bool UpdateDocumentoTipo(DocumentoTipo documentoTipo) {
            string query = "UPDATE TiposDocumento SET tipoDocumentoNome = @TipoDocumentoNome, tipoDocumentoStatus = @TipoDocumentoStatus WHERE tipoDocumentoId = @TipoDocumentoId";
            mySqlCommand = new MySqlCommand(query, database);
            mySqlCommand.Parameters.AddWithValue("@TipoDocumentoNome", documentoTipo.Nome);
            mySqlCommand.Parameters.AddWithValue("@TipoDocumentoStatus", documentoTipo.Status);
            mySqlCommand.Parameters.AddWithValue("@TipoDocumentoId", documentoTipo.Id);
            using (mySqlCommand) {
                return (mySqlCommand.ExecuteNonQuery() > 0);
            }
        }

        public bool? CreateDocumentoTipo(DocumentoTipo documento) {
            string query = "INSERT INTO TiposDocumento() VALUES (null, @TipoDocumentoNome, @TipoDocumentoStatus)";
            mySqlCommand = new MySqlCommand(query, database);
            mySqlCommand.Parameters.AddWithValue("@TipoDocumentoNome", documento.Nome);
            mySqlCommand.Parameters.AddWithValue("@TipoDocumentoStatus", documento.Status);
            using (mySqlCommand) {
                return (mySqlCommand.ExecuteNonQuery() > 0);
            }
        }


        public bool DeleteDocumento(Documento documento) {
            string query = "UPDATE Documentos SET documentoStatus = 0 WHERE documentoId = @DocumentoId";
            mySqlCommand = new MySqlCommand(query, database);
            mySqlCommand.Parameters.AddWithValue("@DocumentoId", documento.Id);
            using (mySqlCommand) {
                return (mySqlCommand.ExecuteNonQuery() > 0);
            }
        }

        public bool DeleteDocumentoTipo(Documento documento) {
            string query = "UPDATE TiposDocumento SET tipoDocumentoStatus = 0 WHERE tipoDocumentoId = @TipoDocumentoId";
            mySqlCommand = new MySqlCommand(query, database);
            mySqlCommand.Parameters.AddWithValue("@TipoDocumentoId", database);
            using (mySqlCommand) {
                return (mySqlCommand.ExecuteNonQuery() > 0);
            }
        }
    }
}
