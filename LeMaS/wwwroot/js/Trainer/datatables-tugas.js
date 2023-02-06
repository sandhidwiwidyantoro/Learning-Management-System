$(document).ready(function () {
    $("#btnSubmit").css('display', 'block');
    $("#btnEdit").css('display', 'none');

    $("#modalInsert").on('hidden.bs.modal', function () {
        $("#namaHeader").html("Insert Data");
        $("#btnSubmit").css('display', 'block');
        $("#btnEdit").css('display', 'none');

        console.log("modal ketutup");
        //atur ulang
        $('#inputId').val(0);
        /*$("#inputId").attr("disabled", "true");*/
/*        document.getElementById("inputId").disabled = false;
*/      $('#inputNamaTugas').val('');
        $('#inputJudul').val('');
        $('#inputNamaFile').val('');
        /*        $("#inputNamaFile").attr("disabled", "false");
                document.getElementById("inputNamaFile").disabled = false;*/
        $('#inputDescTugas').val('');
        $('#inputIdMateri').val('');
        /*        $("#inputIdMateri").attr("disabled", "false");
                document.getElementById("inputIdMateri").disabled = false;*/
    });
});

// ===== UNTUK VALIDASI INSERT DATA=====
$(function () {
    $("#formValidation").validate({
        rules: {
            NamaTugas: {
                required: true
            },
            Judul: {
                required: true
            },
            NamaFile: {
                required: true
            },
            DescTugas: {
                required: true
            },
            IdMateri: {
                required: true
            }
        },
        messages: {
            NamaTugas: {
                required: "<p style='font-size: 13px; color: red; margin-bottom:-50px;'>*Please enter your name tugas</p>"
            },
            Judul: {
                required: "<p style='font-size: 13px; color: red; margin-bottom:-50px;'>*Please enter your judul</p>"
            },
            NamaFile: {
                required: "<p style='font-size: 13px; color: red; margin-bottom:-50px;'>*Please enter your name file</p>"
            },
            DescTugas: {
                required: "<p style='font-size: 13px; color: red; margin-bottom:-50px;'>*Please enter your desc tugas</p>"
            },
            IdMateri: {
                required: "<p style='font-size: 13px; color: red; margin-bottom:-50px;'>*Please enter your id materi</p>"
            }
        }
    });
});

$('#btnSubmit').click(function (e) {
    e.preventDefault();
    if ($('#formValidation').valid() == true) {
        Insert();
    }
    else {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Something went wrong!',
        })
    }
});

$("#btnEdit").click(function (e) {
    e.preventDefault();
    if ($("#formValidation").valid() == true) {
        UpdateData();
    }
    else {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Something went wrong!',
        })
    }
})

// ===== UNTUK VALIDASI INSERT DATA===== end

// ===== UNTUK INSERT DATA===== //
function Insert() {
    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    obj.Id = $("#inputId").val();
    obj.NamaTugas = $("#inputNamaTugas").val();
    obj.Judul = $("#inputJudul").val();
    obj.NamaFile = $("#inputNamaFile").val();
    obj.DescTugas = $("#inputDescTugas").val();
    obj.IdMateri = $("#inputIdMateri").val(),
        console.log(obj);
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
        url: "https://localhost:7230/api/Tugas",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(obj)//jika terkena 415 unsupported media type (tambahkan headertype Json & JSON.Stringify();)
    }).done((result) => {
        //buat alert pemberitahuan jika success
        Swal.fire({
            text: 'Berhasil simpan data',
            icon: 'success',
            timer: 2000,
            timerProgressBar: true
        });
        $('#tugas').DataTable().ajax.reload();
    }).fail((error) => {
        //alert pemberitahuan jika gagal
        Swal.fire({
            text: 'Data gagal disimpan',
            icon: 'error',
            timer: 2000,
            timerProgressBar: true
        });
    })
}
// ===== UNTUK INSERT DATA===== // END


// ===== UNTUK DELETE DATA===== // 
const Delete = (id) => {
    Swal.fire({
        title: 'Are you sure?',
        text: 'You want able to revert this!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'DELETE',
                url: `https://localhost:7230/api/Tugas?id=${id}`,
                success: () => {
                    Swal.fire(
                        'Deleted',
                        'Employee has been deleted.',
                        'success'
                    )
                    $('#tugas').DataTable().ajax.reload()
                },
                error: () => {
                    Swal.fire(
                        'Failed',
                        'Error deleting employee',
                        'error'
                    )
                }
            })
        }
    })
}
// ===== UNTUK DELETE DATA===== // END

// ===== UNTUK UPDATE DATA===== // 
function Update(id) {
    $("#namaHeader").html("Edit Data");
    $("#btnSubmit").css('display', 'none');
    $("#btnEdit").css('display', 'block');
    $.ajax({
        url: "https://localhost:7230/api/Tugas/id?id=" + id,
        success: function (result) {
            console.log(result);
        }
    }).done((result) => {
        $("#inputId").val(result.data.id);
        $("#inputId").attr("disabled", "true");

        $("#inputNamaTugas").val(result.data.namaTugas);
        $("#inputJudul").val(result.data.judul);

        $("#inputNamaFile").val(result.data.namaFile);

        $("#inputDescTugas").val(result.data.descTugas);

        $("#inputIdMateri").val(result.data.idMateri);

    }).fail((err) => {
        console.log(err);
    })
}

function UpdateData() {

    var edt = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    edt.Id = $("#inputId").val();
    edt.NamaTugas = $("#inputNamaTugas").val();
    edt.Judul = $("#inputJudul").val();
    edt.NamaFile = $("#inputNamaFile").val();
    edt.DescTugas = $("#inputDescTugas").val();
    edt.IdMateri = $("#inputIdMateri").val();
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
        url: "https://localhost:7230/api/Tugas",
        type: "PUT",
        contentType: "application/json",
        data: JSON.stringify(edt)//jika terkena 415 unsupported media type (tambahkan headertype Json & JSON.Stringify();)
    }).done((result) => {
        //buat alert pemberitahuan jika success
        console.log("Berhasil simpan data")
        $("#modalInsert").modal("hide");
        Swal.fire({
            text: 'Berhasil simpan data',
            icon: 'success',
            timer: 2000,
            timerProgressBar: true
        });
        $('#tugas').DataTable().ajax.reload();
    }).fail((error) => {
        //alert pemberitahuan jika gagal
        console.log("Data gagal disimpan")
/*        $("#modalInsert").modal("hide");
*/        Swal.fire({
            text: 'Data gagal disimpan',
            icon: 'error',
            timer: 2000,
            timerProgressBar: true
        });
    })
}
// ===== UNTUK UPDATE DATA END===== //
