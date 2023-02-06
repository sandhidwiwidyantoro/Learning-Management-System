$(document).ready(function () {
    $("#showsubmit").css('display', 'block');
    $("#btnEdit").css('display', 'none');

    $("#modalInsert").on('hidden.bs.modal', function () {
        $("#namaHeader").html("Insert Data");
        $("#showsubmit").css('display', 'block');
        $("#menghilang").css('display', 'block');
        $("#btnEdit").css('display', 'none');

        console.log("modal ketutup");
        //atur ulang
        $('#inputId').val(0);
/*        $("#inputId").attr("disabled", "true");
*/
/*        document.getElementById("inputId").disabled = true;
*/      $('#inputNamaMateri').val('');
        $('#inputJudul').val('');
        $('#inputNamaFile').val('');
        // $("#inputPhone").attr("disabled", "false");
        $('#inputDescMateri').val('');
        $('#inputTokenKelas').val('');
        //$("#inputEmail").attr("disabled", "false");
    });


});

// ===== UNTUK VALIDASI INSERT DATA=====
$(function () {
    $("#formValidation").validate({
        rules: {
            NamaMateri: {
                required: true
            },
            Judul: {
                required: true
            },
            NamaFile: {
                required: true
            },
            DescMateri: {
                required: true
            },
            TokenKelas: {
                required: true
            }
        },
        messages: {
            NamaMateri: {
                required: "<p style='font-size: 13px; color: red; margin-bottom:-50px;'>*Please enter your name materi</p>"
            },
            Judul: {
                required: "<p style='font-size: 13px; color: red; margin-bottom:-50px;'>*Please enter your judul</p>"
            },
            NamaFile: {
                required: "<p style='font-size: 13px; color: red; margin-bottom:-50px;'>*Please enter your name file</p>"
            },
            DescMateri: {
                required: "<p style='font-size: 13px; color: red; margin-bottom:-50px;'>*Please enter your desc materi</p>"
            },
            TokenKelas: {
                required: "<p style='font-size: 13px; color: red; margin-bottom:-50px;'>*Please enter your token kelas</p>"
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

let filename = "";

$('#inputfile').change(function () {
    filename = this.files[0].name;
});

// ===== UNTUK INSERT DATA===== //
function Insert() {
    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    obj.Id = 0;
    obj.NamaMateri = $("#inputNamaMateri").val();
    obj.Judul = $("#inputJudul").val();
    obj.NamaFile = filename;
    obj.DescMateri = $("#inputDescMateri").val();
    obj.TokenKelas = parseInt($("#inputTokenKelas").val()),
        console.log(obj);
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
        url: "https://localhost:7230/api/Materi",
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
                url: `https://localhost:7230/api/Materi?id=${id}`,
                success: () => {
                    Swal.fire(
                        'Deleted',
                        'Employee has been deleted.',
                        'success'
                    )
                    $('#materi').DataTable().ajax.reload()
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
let editnamafile = "";
function Update(id) {
    $("#namaHeader").html("Edit Data");
    $("#submitaja").css('display', 'none');
    $("#btnEdit").css('display', 'block');
    $("#menghilang").css('display', 'none');
    $("#showsubmit").css('display', 'none');
    $.ajax({
        url: "https://localhost:7230/api/Materi/id?id=" + id,
        success: function (result) {
            console.log(result);
        }
    }).done((result) => {
        $("#inputId").val(result.data.id);              

        $("#inputNamaMateri").val(result.data.namaMateri);

        $("#inputJudul").val(result.data.judul);

        $("#inputDescMateri").val(result.data.descMateri);

        $("#inputTokenKelas").val(result.data.tokenKelas);
        editnamafile = result.data.namaFile;
        console.log(editnamafile);
    }).fail((err) => {
        console.log(err);
    })
}

function UpdateData() {

    var edt = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    edt.Id = parseInt($("#inputId").val());
    edt.NamaMateri = $("#inputNamaMateri").val();
    edt.Judul = $("#inputJudul").val();
    edt.NamaFile = editnamafile;
    edt.DescMateri = $("#inputDescMateri").val();
    edt.TokenKelas = parseInt($("#inputTokenKelas").val());
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
        url: "https://localhost:7230/api/Materi",
        type: "PUT",
        contentType: "application/json",
        data: JSON.stringify(edt)//jika terkena 415 unsupported media type (tambahkan headertype Json & JSON.Stringify();)
    }).done((result) => {
        //buat alert pemberitahuan jika success
        console.log("Berhasil simpan data")
/*        $("#modalInsert").modal("hide");
*/        Swal.fire({
            text: 'Berhasil simpan data',
            icon: 'success',
            timer: 2000,
            timerProgressBar: true
        });
        $('#materi').DataTable().ajax.reload();
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
