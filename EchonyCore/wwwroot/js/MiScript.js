$('document').ready(() => {
    /*
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
   
    $(document).on("click", ".pruebaBoton", (event) => {
       
        let aceptar = {}
        aceptar.Id = event.target.id;
        $.ajax({
            method: "POST",
            url: "/Social/AceptarSolicitud",
            data: aceptar,
            datatype: "JSON",
            success: (data) => {
                console.log(data);
            },
            error: (error) => {
                console.log(error)
            },
            complete: (error, resp) => {

                console.log(resp)
            }
        });
        event.target.remove();
    });


  */
    $(document).on("click", ".me_gusta_btn", (event) => {
       
        let like = {}
        like.PublicacionesId = event.target.id;
        like.UsuarioId = event.target.name;
        $("#load_gustaBtn_" + like.PublicacionesId).attr("display", "inline-flex");
        $("#load_gustaBtn_" + like.PublicacionesId).show();


        $.ajax({
            method: "POST",
            url: "/Social/MeGusta",
            data: like,
            datatype: "JSON",
            success: (data) => {
                let listaLikes = JSON.parse(data);
                console.log(listaLikes);
                $("." + event.target.id + "").html("(" + listaLikes.length + ")");
               
                $("#load_gustaBtn_" + like.PublicacionesId).hide();
            },
            error: (error) => {
                console.log(error.message);
                $("#load_gustaBtn_" + like.PublicacionesId).hide();
            },
            complete: (error, resp) => {
                $("#load_gustaBtn_" + like.PublicacionesId).hide();
                console.log(resp)
            }
        });
   
    });

    $(document).on("click", ".mostrar_likes", (event) => {

        let like = {}
        like.PublicacionesId = event.target.name;
       
  

      

        $.ajax({
            method: "POST",
            url: "/Social/MegustaInfo",
            data: like,
            datatype: "JSON",
            success: (data) => {
                let listaLikes = JSON.parse(data);
                console.log(listaLikes);
                for (let i = 0; i < listaLikes.length; i++) {
                    console.log();
                }
                $("#mostrar_likes_" + like.PublicacionesId).attr("open", "true");
                $("#mostrar_likes_body_" + like.PublicacionesId).html("");
                listaLikes.forEach(e => {
                    console.log(e.Usuario.Avatar);
                    if (e.Usuario != null){
                        let elemento = `
                           <div class="like_tarjeta">
                                <div>
                                    <img class="foto_like"   src = "/images/Fotos_Usuarios/${e.Usuario.Avatar}" />
                                </div>
                                <div>
                                    <h3>${e.Usuario.Nombre} ${e.Usuario.Apellido}</h3>
                                </div>
                            </div>
                                `;
                        $("#mostrar_likes_body_" + like.PublicacionesId).append(elemento);
                    } else {
                        let elemento = `
                           <div class="like_tarjeta">
                                
                                <div>
                                    <h3>Esta publicacion aún no tiene likes</h3>
                                </div>
                            </div>
                                `;
                        $("#mostrar_likes_body_" + like.PublicacionesId).append(elemento);
                    }
                   
                    
                })
               
              
                

               

                $("#load_gustaBtn_" + like.PublicacionesId).hide();
            },
            error: (error) => {
                console.log(error.message);
                $("#load_gustaBtn_" + like.PublicacionesId).hide();
            },
            complete: (error, resp) => {
                $("#load_gustaBtn_" + like.PublicacionesId).hide();
                console.log(resp)
            }
        });

    });

    /*MODALES*/

    $("#btnFoto").on("click", () => {
        $("#modal_foto").attr("open", "true");
    });

    $("#cerrar_modal").on("click", () => {
        $("#modal_foto").removeAttr("open");

    });

    $("#cerrar_dialog_likes").on("click", () => {
        $(".dialog_likes").removeAttr("open");
    });

    $(document).on("click", ".cerrar_likes", (event) => {
        let id = event.target.id;
     
        $("#mostrar_likes_" +id).removeAttr("open");
    });
    
    $(document).on("click", ".cerrar_publicacion", (event) => {
        let id = event.target.id;

        $("#post_" + id).removeAttr("open");
    });
});

