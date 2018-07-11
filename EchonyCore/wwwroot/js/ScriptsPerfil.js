$('document').ready(() => {
    /*
    $texto = $("#contenido-post");
    $btn = $("#publicar-post");
    $idPerfil = $("#idPerfil");
    $idPerfil.hide();

    */

    /* $texto.keypress(e => {
         var code = (e.keyCode ? e.keyCode : e.which);
         if (code == 13) {
             guardarPost();
         }
     })*/

   /* $("#publicar-post").on("click", () => {
        guardarPost();
        mostrarPublicaciones();

    });*/
    /*
    $("#load").hide();
    $("#publicar-post").hide();
    $("#foto-post").hide();
    $texto.on('click', () => {
        $("#publicar-post").show();
        $("#foto-post").show();
    });
    var date = new Date();
    const guardarPost = () => {
        $publicacion = $texto.val();
      
        let Publicacion = {
            Contenido: $publicacion,
            Fecha: date,
            UsuarioId: $idPerfil.val()
        }
        console.log(Publicacion);
        $("#load").show();
        console.log("Cargando..");
        $.ajax({
            datatype: "json",
            type: "post",
            url: "/Perfil/AddPublicacion",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(Publicacion),
            success: data => {
              
                console.log(data);
                $("#load").hide();
            },
            error: e => {
                console.log("Termino de cargar..");
                $("#load").hide();
            }
        });
        $placeholder = $("#contenido-post").attr("placeholder");

        $texto.val('');
        $texto.attr("placeholder", $placeholder);

        
    }

    $loadingContainer = $("#loadContainer");
    $("#Nick").hide();
    $loadingContainer.hide();
    mostrarPublicaciones = () => {
        $loadingContainer.show();
        let Nick = {
            NickName: $("#Nick").val()
        };
       
        $.ajax({
            type: "post",
            datatype: "json",
            url: '/Perfil/GetPublicacion',
            data: JSON.stringify(Nick),
            success: data => {
                $loadingContainer.hide();
                DibujadorPublicacion(JSON.parse(data));
             
               /* for (let i = 0; i < data.length; i++) {
                    console.log(data[i].Post_Usuario);
                }*/
                /*
               
            },
            error: e => {
                $loadingContainer.hide();
                console.log("Error en la peticion AJAX: "+e.error);
            }
        });
    }

    DibujadorPublicacion = (data) => {
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
            for (let i = 0; i < data.reverse().length; i++) {
                let fecha = JsonDate(data[i].Fecha);
                
            let elemento = ` 
        <div class="post-wrapper">
            

            <article id="post" class="post">
                <div class="post-info">
                    <img src="../Fotos_Usuarios/${data[i].RutaFoto}" alt="Foto" />
                    <span>${data[i].Nombre}</span> | <p>${fecha}</p>
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
                <img src="..\\wwwroot\\images\\Fotos_Usuarios\\${data[i].RutaFoto}" alt="Foto" />
                <div class="comment-texarea">

                  <div class="contenido-comentario-container"><span>${data[i].Nombre} </span><span>${data[i].Apellido}</span><p>Este es un comentario </p></div>
                </div>
                </div>

            </div>
            <form method="post" id="comentario_form" action="AddComentario/Perfil">
            
            <input type="hidden" name="idPublicacion" value ="${data[i].Id}" />
            <input type="hidden" name="idUsuario" value ="${$idUsuario}" />
            <div id="post-comment" class="post-comment">
                <img src="#" alt="Foto" />
                <div class="comment-texarea">

                    
                    <input name="comentario" type="text" placeholder="comenta" />
                </div>

            </div>
             <input id="btnEnviar-comentario" type="submit" value="Enviar" />
            </form>
        </div>
                
          
        `;

          $("#post_container").append(elemento);
       
            }
        }
    };*/
    

   //<textarea name="comentario" placeholder="Comenta esta publicación..."></textarea>
    /*const agregadorComentarios = () => {
        $.ajax({
            datatype: "JSON",
            type: "POST",
            url: "AddComentario/Perfil",
            data: $("#comentario_form").serialize(),
            success: (data) => {
                console.log(data);
            },
            error: (error) => {
                console.log(error);
            },
            complete: (error, data) => {
                console.log(error, data);
            }
        });
        return false;
    }
    $("#btnEnviar-comentario").on("click", agregadorComentarios());
    */


   // $("body").on("load", mostrarPublicaciones());

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

    $("#btnFoto").on("click", () => {
        $("#modal_foto").attr("open", "true");
    });

    $("#cerrar_modal").on("click", () => {
        $("#modal_foto").removeAttr("open");
    });

    });
   
});

/* */