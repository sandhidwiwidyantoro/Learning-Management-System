using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class newmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_employee",
                columns: table => new
                {
                    nik = table.Column<string>(type: "nchar(5)", nullable: false),
                    firstname = table.Column<string>(name: "first_name", type: "nvarchar(30)", maxLength: 30, nullable: false),
                    lastname = table.Column<string>(name: "last_name", type: "nvarchar(30)", maxLength: 30, nullable: true),
                    gender = table.Column<int>(type: "int", nullable: false),
                    birthdate = table.Column<DateTime>(name: "birth_date", type: "date", nullable: false),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idrole = table.Column<int>(name: "id_role", type: "int", nullable: false),
                    isactive = table.Column<bool>(name: "is_active", type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_employee", x => x.nik);
                    table.UniqueConstraint("AK_tb_m_employee_email", x => x.email);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_batchclass",
                columns: table => new
                {
                    tokenkelas = table.Column<string>(name: "token_kelas", type: "nchar(7)", nullable: false),
                    NoBatch = table.Column<int>(type: "int", nullable: false),
                    NamaBatch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JenisKelas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PIC = table.Column<string>(type: "nchar(5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_batchclass", x => x.tokenkelas);
                    table.ForeignKey(
                        name: "FK_tb_m_batchclass_tb_m_employee_PIC",
                        column: x => x.PIC,
                        principalTable: "tb_m_employee",
                        principalColumn: "nik");
                });

            migrationBuilder.CreateTable(
                name: "tb_m_materi",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NamaMateri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Judul = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NamaFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descmateri = table.Column<string>(name: "desc_materi", type: "text", nullable: false),
                    TokenKelas = table.Column<string>(type: "nchar(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_materi", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_m_materi_tb_m_batchclass_TokenKelas",
                        column: x => x.TokenKelas,
                        principalTable: "tb_m_batchclass",
                        principalColumn: "token_kelas");
                });

            migrationBuilder.CreateTable(
                name: "tb_m_participant",
                columns: table => new
                {
                    nik = table.Column<string>(type: "nchar(5)", nullable: false),
                    FinalScore = table.Column<int>(type: "int", nullable: false),
                    IdBatchClass = table.Column<string>(type: "nchar(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_participant", x => x.nik);
                    table.ForeignKey(
                        name: "FK_tb_m_participant_tb_m_batchclass_IdBatchClass",
                        column: x => x.IdBatchClass,
                        principalTable: "tb_m_batchclass",
                        principalColumn: "token_kelas");
                    table.ForeignKey(
                        name: "FK_tb_m_participant_tb_m_employee_nik",
                        column: x => x.nik,
                        principalTable: "tb_m_employee",
                        principalColumn: "nik");
                });

            migrationBuilder.CreateTable(
                name: "tb_m_tugas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NamaTugas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Judul = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NamaFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    desctugas = table.Column<string>(name: "desc_tugas", type: "text", nullable: true),
                    IdMateri = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_tugas", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_m_tugas_tb_m_materi_IdMateri",
                        column: x => x.IdMateri,
                        principalTable: "tb_m_materi",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tb_r_participanttugas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nilai = table.Column<int>(type: "int", nullable: false),
                    NamaFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTugas = table.Column<int>(type: "int", nullable: false),
                    IdPeserta = table.Column<string>(type: "nchar(5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_r_participanttugas", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_r_participanttugas_tb_m_participant_IdPeserta",
                        column: x => x.IdPeserta,
                        principalTable: "tb_m_participant",
                        principalColumn: "nik");
                    table.ForeignKey(
                        name: "FK_tb_r_participanttugas_tb_m_tugas_IdTugas",
                        column: x => x.IdTugas,
                        principalTable: "tb_m_tugas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_batchclass_PIC",
                table: "tb_m_batchclass",
                column: "PIC");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_materi_TokenKelas",
                table: "tb_m_materi",
                column: "TokenKelas");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_participant_IdBatchClass",
                table: "tb_m_participant",
                column: "IdBatchClass");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_tugas_IdMateri",
                table: "tb_m_tugas",
                column: "IdMateri");

            migrationBuilder.CreateIndex(
                name: "IX_tb_r_participanttugas_IdPeserta",
                table: "tb_r_participanttugas",
                column: "IdPeserta");

            migrationBuilder.CreateIndex(
                name: "IX_tb_r_participanttugas_IdTugas",
                table: "tb_r_participanttugas",
                column: "IdTugas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_r_participanttugas");

            migrationBuilder.DropTable(
                name: "tb_m_participant");

            migrationBuilder.DropTable(
                name: "tb_m_tugas");

            migrationBuilder.DropTable(
                name: "tb_m_materi");

            migrationBuilder.DropTable(
                name: "tb_m_batchclass");

            migrationBuilder.DropTable(
                name: "tb_m_employee");
        }
    }
}
