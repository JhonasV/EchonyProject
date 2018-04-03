$('document').ready(() => {

    $("#load").hide();
    const guardar = () => {
        $("#load").show();
        let date = new Date();
        let datos = {
            UsuarioId: $("#idPerfil").val(),
            Contenido: $("#contenido-post").val(),
            Fecha: date
        };
        $.ajax({
            url: "/Perfil/AddPublicacion",
            type: "POST",
            datatype: "JSON",
            data: JSON.stringify(datos),
            success: data => {
                console.log(data);
                $("#load").hide();
            },
            error: e => {
                $("#load").hide();
                console.log(e);
            }
        });
        $placeholder = $("#contenido-post").attr("placeholder");

        $("#contenido-post").val('');
        $("#contenido-post").attr("placeholder", $placeholder);
    };
    $("#load").hide();
    const Publicacion = () => {
        $("#load").show();
        let datos = {
            UsuarioId: $("#idPerfil").val(),
            Contenido: $("#contenido-post").val(),
            Fecha: new Date()
        };
        $.post("/Perfil/AddPublicacion", datos).done((data) => {
            if (data.Ok) {
                $("#load").hide();
                console.log(data);
            } else {
                $("#load").hide();
                console.log("error");
            }
        }).fail((error) => {
            $("#load").hide();
            console.log(error);
        });
        $placeholder = $("#contenido-post").attr("placeholder");

        $("#contenido-post").val('');
        $("#contenido-post").attr("placeholder", $placeholder);
    };
    $("#loadContainer").hide();
 

    const mostrarPublicacionesDos = () => {
        $("#loadContainer").show();
        let datos = {
            NickName: $("#Nick").val()
        };
        $.ajax({
            url: "/Perfil/GetPublicacion",
            type: "POST",
            datatype: "JSON",
            data: datos,
            success: data => {
                DibujadorPublicacion(JSON.parse(data));
                $("#loadContainer").hide();
            },
            error: e => {
                $("#loadContainer").hide();
                console.log(e);
            }
        });
    }

    const DibujadorPublicacion = (data) => {
        $idUsuario = $("#idPerfil").val();

        $("#post_container").html("");

        if (data.length === 0) {
            $("#post_container").html("");
            let noPublicaciones = ` 
                <div class="aviso">
                        <p>No ha hecho su primera publicación</p>
                </div>
            `;
            $("#post_container").append(noPublicaciones);
        } else {


            data.reverse().length
            for (let i = 0; i < data.length; i++) {
               // let fecha = JsonDate(data[i].Fecha);

                let elemento = ` 
        <div class="post-wrapper">
            

            <article id="post" class="post">
                <div class="post-info">
                    <img src="../images/Fotos_Usuarios/${data[i].RutaFoto}" alt="Foto" />
                    <span>${data[i].Nombre}</span> | <p></p>
                    <div id="btn-post"><a  href="#">Me Gusta</a><a  href="#">Comentar</a></div>
                   
                </div>
                <div class="post-content">
                    ${data[i].Contenido}
                </div>
                <div class="botonera-publiacion">
                    <button>Me Gusta</button>
                    <button>Compartir</button>
                    <button id="click-comentar">Comentar</button>
                </div>
                 <input type="hidden" value="${data[i].Id}"/> 
                        </article>
                <div class="coment-wrapper">
                <div id="post-comment" class="post-comment">
                <img src="../images/Fotos_Usuarios/${data[i].RutaFoto}" alt="Foto" />
                <div class="comment-texarea">

                  <div class="contenido-comentario-container"><span>${data[i].Nombre} </span><span>${data[i].Apellido}</span><p>Este es un comentario </p></div>
                </div>
                </div>

            </div>
            <form method="post" id="comentario_form" action="AddComentario/Perfil">
            
            <input type="hidden" name="idPublicacion" value ="${data[i].Id}" />
            <input type="hidden" name="idUsuario" value ="${$idUsuario}" />
            <div id="post-comment" class="post-comment">
                <img src="../images/Fotos_Usuarios/${data[i].RutaFoto}" alt="Foto" />
                <div class="comment-texarea">

                    
                    <input name="comentario" type="text" placeholder="comenta" />
                </div>

            </div>
             <input id="btnEnviar-comentario" type="submit" value="Enviar" />
            </form>
        </article>
        </div>
                
          
        `;

                $("#post_container").append(elemento);

            }
        }
    };

    
    //Formateador de fechas JSON
    const JsonDate = (jsonDate) => {
        var offset = new Date().getTimezoneOffset() * 60000;
        var parts = /\/Date\((-?\d+)([+-]\d{2})?(\d{2})?.*/.exec(jsonDate);
        if (parts[2] === undefined) parts[2] = 0;
        if (parts[3] === undefined) parts[3] = 0;
        d = new Date(+parts[1] + offset + parts[2] * 3600000 + parts[3] * 60000);
        date = d.getDate() + 1;
        date = date < 10 ? "0" + date : date;
        mon = d.getMonth() + 1;
        mon = mon < 10 ? "0" + mon : mon;
        year = d.getFullYear();
        return (date + "/" + mon + "/" + year);
    };

   /* $("#publicar-post").on("click", () => {
       // Publicacion();
        guardar();
        mostrarPublicacionesDos()
       
    });*/
  
    //$("body").on("load", mostrarPublicacionesDos());


    /*Envío de solicitud*/
    /*$("#envio_form").submit( (e) => {
        let form = $(this);
       

        form.ajaxSubmit({
            datatype: "JSON",
            type: "POST",
            url: form.attr("action"),
            success: (data) => {
                console.log(data);
            },
            error: (e) => {
                console.log(e.message);
            },
            complete: (e, data) => {
                console.log(e, data);
            }
        });
        return false;

       /* try {


            $.ajax({
                url: $(this).attr("action").val(),
                data: $(this).serialize(),
                datatype: "JSON",
                type: "POST",
                success: (data) => {
                    console.log(data);
                },
                error: (e) => {
                    console.log(e.message);
                },
                complete: (e, data) => {
                    console.log(e, data);
                }
            });
        } catch (ex) {
            console.log(ex);
        }
        
       
    });*/
   /* $("#envio_form").submit(() => {
        let form = $(this);

        if (form.validate()) {
            form.ajaxSubmit({
                dataType: 'JSON',
                type: 'POST',
                url: form.attr('action'),
                success: (r) => {
                    if (r.respuesta) {
                        console.log(r);
                    } else {
                        console.log(r.error);
                    }
                },
                error: (jqXHR, textStatus, errorThrown) => {
                    alert(errorThrown);
                }
            });
        }
        return false;
    });*/

    const Cancelar = (e) => {

        $("#cancelar_form").on("submit", function (e) {
            e.preventDefault();
            let form = $("#cancelar_form");
            $("#cancelar_form").hide();
            $("#envio_form").hide();
            $.ajax({
                url: "/Perfil/EliminarSolicitud",
                data: form.serialize(),
                datatype: "JSON",
                method: "POST",
                success: (data) => {
                    console.log("CANCELAR: " + JSON.parse(data));
                    dibujarBotonSolicitud(JSON.parse(data), this);
                },
                error: (e) => {
                    console.log(e.message);
                },
                complete: (e, data) => {
                    console.log(e, data);
                }
            });

        });
    }

    $("#cancelar_form").on("submit", function (e) {
        e.preventDefault();
        let form = $("#cancelar_form");
        $("#cancelar_form").hide();
        $("#envio_form").hide();
        $.ajax({
            url: "/Perfil/EliminarSolicitud",
            data: form.serialize(),
            datatype: "JSON",
            method: "POST",
            success: (data) => {
                console.log("CANCELAR: " + JSON.parse(data));
                dibujarBotonSolicitud(JSON.parse(data), this);
            },
            error: (e) => {
                console.log(e.message);
            },
            complete: (e, data) => {
                console.log(e, data);
            }
        });

    });

    const enviar = (e) => {
        $("#envio_form").on("submit", function (e) {
            e.preventDefault();
            let form = $("#envio_form");
            let url = form.attr("action");
            let metodo = form.attr("method");

            $.ajax({
                method: "POST",
                url: "/Perfil/NotificacionesJson",
                data: form.serialize(),
                datatype: "JSON",
                success: (data) => {
                    console.log("ENVIO: " + JSON.parse(data));
                    dibujarBotonSolicitud(JSON.parse(data), this);

                },
                error: (error) => {
                    console.log(error)
                },
                complete: (error, resp) => {

                    console.log(resp)
                }
            });

        });
    }
 
    $("#envio_form").on("submit", function (e) {
        e.preventDefault();
        let form = $("#envio_form");
        let url = form.attr("action");
        let metodo = form.attr("method");

        $.ajax({
            method: "POST",
            url: "/Perfil/NotificacionesJson",
            data: form.serialize(),
            datatype: "JSON",
            success: (data) => {
                console.log("ENVIO: " + JSON.parse(data));
                dibujarBotonSolicitud(JSON.parse(data), this);

            },
            error: (error) => {
                console.log(error)
            },
            complete: (error, resp) => {
               
                console.log(resp)
            }
        });
       
    });
   

    const dibujarBotonSolicitud = (resp) => {
        //e.preventDefault();
        let Emisor = {}
        let Receptor = {}
        let contenedor = $("#btn_sol_container");
        Emisor.EmisorId = $("#EmisorId").val();
        Receptor.ReceptorId = $("#ReceptorId").val();
        let NickName = $("#GetNick").val();

       /* $("#cancelar_amigo").addClass("ocultarBoton");
        $("#cancelar_form").addClass("ocultarBoton");
        $("#envio_form").addClass("ocultarBoton");*/

       contenedor.html("");

        //$("#cancelar_form").hide();
        //$("#envio_form").hide();

        let SolicitudEnviada =
            `
            <form id="cancelar_form" action="/Perfil/EliminarSolicitud" method="post">
                  <input type="hidden" name="Id" value="${resp.Id}" />
                  <input type="submit" onclick="Cancelar(this);" name="agregar" value="Cancelar Solicitud" />
            </form>
            `;
        let SonAmigos =
            `
            <form id="cancelar_form" action="/Perfil/EliminarSolicitud" method="post">
                  <input type="hidden" name="Id" value="${resp.Id}" />
                  <input type="submit" name="agregar" value="Eliminar amigo" />
            </form>
            `;
       
        let NoSonAmigos = `
                            <form id="envio_form" class="envio_form" action="/Perfil/NotificacionesJson" method="post">
                                <input type="hidden" name="EmisorId" value="${Emisor.EmisorId}" />
                                <input type="hidden" name="ReceptorId" value="${Receptor.ReceptorId}" />
                                <input type="hidden" name="NickName" value="${NickName}" />
                                <input type="hidden" name="Estado" value="0" />
                                <input id="boton_enviar" type="submit" value="Enviar solicitud de amistad" />
                            </form>
                `;
        console.log(resp.Estado);
        
        if (resp.Estado == 5) {
            contenedor.append(NoSonAmigos);
            
        } else if (resp.Estado == 0) {
            contenedor.append(SolicitudEnviada);
           
        } else {
            contenedor.append(SonAmigos);
        }
        
        return false;
    }

    /*MODALES*/

    $("#btnFoto").on("click", () => {
        $("#modal_foto").attr("open", "true");
    });

    $("#cerrar_modal").on("click", () => {
        $("#modal_foto").removeAttr("open");

    });

});

