$(document).ready(function () {
    $("#btnSubmit").css('display', 'block');
    $("#btnEdit").css('display', 'none');

    $("#modalInsert").on('hidden.bs.modal', function () {
        $("#namaHeader").html("Insert Data");
        $("#btnSubmit").css('display', 'block');
        $("#btnEdit").css('display', 'none');

        console.log("modal ketutup");
        //atur ulang
        $('#inputTokenKelas').val('');
/*        $("#inputId").attr("disabled", "true");
*/        document.getElementById("inputTokenKelas").disabled = false;
        $('#inputNoBatch').val('');
        $('#inputNamaBatch').val('');
        $('#inputJenisKelas').val('');
/*        $("#inputNamaFile").attr("disabled", "false");
        document.getElementById("inputNamaFile").disabled = false;*/
        $('#inputPIC').val('');
/*        $("#inputIdMateri").attr("disabled", "false");
        document.getElementById("inputIdMateri").disabled = false;*/
    });

    let tables = $('#kelas').DataTable({
        ajax: {
            url: "https://localhost:7230/api/BatchClass",
            dataType: "Json",
            dataSrc: "data" //need notice, kalau misal API kalian 
        },
        columns: [ // untuk nampilkan nama kolom di datatable
            {
                //Tugas buat numbering otomatis pada datatables !!!!
                "data": null, "sortable": false,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                "data": "tokenKelas"
            },
            {
                "data": "noBatch"
            },
            {
                "data": "namaBatch"
            },
            {
                "data": "jenisKelas"
            },
            {
                "data": "pic"
            },
            {
                "data": "tokenKelas",
                render: function (data, type, row) {
                    return `<button type="button" onclick="Update(\'${data}'\)" class="btn btn-success" style="width: 100px" data-bs-toggle="modal" data-bs-target="#modalInsert">Edit</button>
                            <button type="button" onclick="Delete(\'${data}'\)" class="btn btn-danger"  style="width: 100px">Hapus</button>`;
                }
            },
        ],
        dom: 'Bfrtip',
        buttons: [
            ['pageLength'],
            {
                extend: 'copyHtml5',
                text: ' Copy',
                className: 'fa fa-files-o bg-info text-white text-uppercase', //nama class button saja
                exportOptions: {
                    columns: [0, ':visible']
                }
            },
            {
                extend: 'excelHtml5',
                text: ' Excel',
                className: 'fa fa-file-excel-o bg-success text-white text-uppercase', //nama class button saja
                exportOptions: {
                    columns: [0, ':visible']
                }
            },
            {
                extend: 'csvHtml5',
                text: ' CSV',
                className: 'fa fa-file-text-o bg-warning text-white text-uppercase', //nama class button saja
                exportOptions: {
                    columns: [0, ':visible']
                }
            },
            {
                extend: 'pdfHtml5',
                text: ' PDF',
                className: 'fa fa-file-pdf-o bg-danger text-white text-uppercase', //nama class button saja
                exportOptions: {
                    columns: [0, ':visible']
                }
            },
            {
                extend: 'colvis',
                className: 'bnt btn-dark mx-2 rounded-pill',
                text: 'Column Visibility',
            }
        ]
    });
});

// ===== UNTUK VALIDASI INSERT DATA=====
$(function () {
    $("#formValidation").validate({
        rules: {
            TokenKelas: {
                required: true,
                minlength: 7,
                maxlength: 7
            },
            NoBatch: {
                required: true
            },
            NamaBatch: {
                required: true
            },
            JenisKelas: {
                required: true
            },
            PIC: {
                required: true
            }
        },
        messages: {
            TokenKelas: {
                required: "<p style='font-size: 13px; color: red; margin-bottom:-50px;'>*Please enter your token kelas</p>",
                minlength: "<p style='font-size: 12px; color: red; margin-bottom:-50px;'>*tokenkelas should be at least 7 number</p>",
                maxlength: "<p style='font-size: 12px; color: red; margin-bottom:-50px;'>*tokenkelas can't be longer than 7 number</p>"
            },
            NoBatch: {
                required: "<p style='font-size: 13px; color: red; margin-bottom:-50px;'>*Please enter your no batch</p>"
            },
            NamaBatch: {
                required: "<p style='font-size: 13px; color: red; margin-bottom:-50px;'>*Please enter your nama batch</p>"
            },
            JenisKelas: {
                required: "<p style='font-size: 13px; color: red; margin-bottom:-50px;'>*Please enter your jenis kelas</p>"
            },
            PIC: {
                required: "<p style='font-size: 13px; color: red; margin-bottom:-50px;'>*Please enter your PIC</p>"
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
    obj.TokenKelas = $("#inputTokenKelas").val();
    obj.NoBatch = $("#inputNoBatch").val();
    obj.NamaBatch = $("#inputNamaBatch").val();
    obj.JenisKelas = $("#inputJenisKelas").val();
    obj.PIC = $("#inputPIC").val();
        console.log(obj);
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
        url: "https://localhost:7230/api/BatchClass",
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
        $('#kelas').DataTable().ajax.reload();
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
                url: `https://localhost:7230/api/BatchClass?id=${id}`,
                success: () => {
                    Swal.fire(
                        'Deleted',
                        'Employee has been deleted.',
                        'success'
                    )
                    $('#kelas').DataTable().ajax.reload()
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
function Update(tokenKelas) {
    $("#namaHeader").html("Edit Data");
    $("#btnSubmit").css('display', 'none');
    $("#btnEdit").css('display', 'block');
    $.ajax({
        url: "https://localhost:7230/api/BatchClass/id?id=" + tokenKelas,
        success: function (result) {
            console.log(result.data);
        }
    }).done((result) => {
        $("#inputTokenKelas").val(result.data.tokenKelas);
        $("#inputTokenKelas").attr("disabled", "true");

        $("#inputNoBatch").val(result.data.noBatch);
        $("#inputNamaBatch").val(result.data.namaBatch);

        $("#inputJenisKelas").val(result.data.jenisKelas);

        $("#inputPIC").val(result.data.pic);

    }).fail((err) => {
        console.log(err);
    })
}

function UpdateData() {

    var edt = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    edt.TokenKelas = $("#inputTokenKelas").val();
    edt.NoBatch = $("#inputNoBatch").val();
    edt.NamaBatch = $("#inputNamaBatch").val();
    edt.JenisKelas = $("#inputJenisKelas").val();
    edt.PIC = $("#inputPIC").val();
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
        url: "https://localhost:7230/api/BatchClass",
        type: "PUT",
        contentType: "application/json",
        data: JSON.stringify(edt)//jika terkena 415 unsupported media type (tambahkan headertype Json & JSON.Stringify();)
    }).done((result) => {
        //buat alert pemberitahuan jika success
        console.log("Berhasil simpan data")
        /*$("#modalInsert").modal("hide");*/
        Swal.fire({
            text: 'Berhasil simpan data',
            icon: 'success',
            timer: 2000,
            timerProgressBar: true
        });
        $('#kelas').DataTable().ajax.reload();
    }).fail((error) => {
        //alert pemberitahuan jika gagal
        console.log("Data gagal disimpan")
        /*$("#modalInsert").modal("hide");*/
        Swal.fire({
            text: 'Data gagal disimpan',
            icon: 'error',
            timer: 2000,
            timerProgressBar: true
        });
    })
}
// ===== UNTUK UPDATE DATA END===== //
