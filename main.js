$(document).ready(function() {

    let getInfo = null;
    (getInfo = function (card = false, list = false, multiple = 1){
        let url = 'https://backend.daviva.lt/API/InformacijaTestui';

        for (i=1; i<=multiple; i++) {
            $.get(url, function (data) {
                let {marke, modelis, metai, nuotraukos, kaina} = data;

                if (card) {
                    $(cardTemplate(marke, modelis, metai, nuotraukos, kaina)).appendTo($("div#cards"));
                    $("html,body").scrollTop($('html,body').prop('scrollHeight'));
                }
                if (list) {
                    $(listTemplate(marke, modelis, metai, nuotraukos, kaina)).appendTo($("tbody#list"));
                    $("#list-scroll").scrollTop($('#list').prop('scrollHeight'));
                }
                tableHeader();
            }, "json");
        }
    })();

    getInfo(card = true, list = false, multiple = 4);
    getInfo(card = false, list = true, multiple = 4);

    $("button#addCard").on("click", function(){
        getInfo(card = true, list = false, multiple = $("#cardNumber").val());
        $('html,body').animate({scrollTop: $('html,body').prop('scrollHeight')}, 'slow');
    });

    $("button#addRow").on("click", function(){
        getInfo(card = false, list = true, multiple = $("#listNumber").val());
        
        //$('.table-responsive,#list').animate({scrollTop: $('#list').prop('scrollHeight')}, 'fast');
    });

    (tableHeader = function () {
        for (var i = 1; i <= 5; i++)
        {
            $("#th"+i).width($("#td"+i).width()); 
        }
    })();

    function cardTemplate(marke, modelis, metai, nuotraukos, kaina) {
        let template = `
                    <div class="card shadow-sm m-3" style="width: 18rem;">
                        <div id="carousel-${nuotraukos[0].slice(-13)}" class="carousel slide card-img-top" data-ride="carousel">
                            <div class="carousel-inner rounded-top">`;
                                nuotraukos.forEach(function(nuotrauka, i) { 
                                    let active = i == 0 ? 'active' : '';
                                    template += `
                                        <div class="carousel-item ${active}">
                                            <a href="" data-toggle="modal" data-target="#modal-${nuotraukos[0].slice(-13)}"><img src="${nuotrauka}" class="d-block w-100"></a>
                                        </div>`;
                                });
            template += `
                            </div>
                            <a class="carousel-control-prev" href="#carousel-${nuotraukos[0].slice(-13)}" role="button" data-slide="prev">
                                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                <span class="sr-only">Previous</span>
                            </a>
                            <a class="carousel-control-next" href="#carousel-${nuotraukos[0].slice(-13)}" role="button" data-slide="next">
                                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                <span class="sr-only">Next</span>
                            </a>
                        </div>

                        <div class="card-body">
                            <h5 class="card-title">${marke}</h5>
                            <div class="card-text">Modelis: ${modelis}</div>
                            <div class="card-text">Metai: ${metai}</div>
                        </div>
                        <div class="card-footer p-0">
                            <div class="text-success h5 text-center p-1 my-2">${kaina} €</div>
                        </div>
                    </div>`;
            template += `
                    <!-- Modal -->
                    <div class="modal fade" id="modal-${nuotraukos[0].slice(-13)}" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog modal-xl" role="document">
                            <div class="modal-content">
                                <div class="modal-header bg-dark p-1 pr-2 d-none">
                                    <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div id="carousel2-${nuotraukos[0].slice(-13)}" class="carousel slide" data-ride="carousel">
                                    <ol class="carousel-indicators">`;

                                    nuotraukos.forEach(function(nuotrauka, i) { 
                                        let active = i == 0 ? 'active' : '';
                                            template += `
                                                <li data-target="#carousel2-${nuotraukos[0].slice(-13)}" data-slide-to="${i}" class="${active}"></li>`;
                                    });
                        template += `
                                    </ol>
                                    <div class="carousel-inner">`;
                                        nuotraukos.forEach(function(nuotrauka, i) { 
                                            let active = i == 0 ? 'active' : '';
                                            template += `
                                                        <div class="carousel-item ${active}">
                                                            <img src="${nuotrauka}" class="d-block w-100">
                                                        </div>`;
                                        });
                        template += `
                                    </div>
                                    <a class="carousel-control-prev" href="#carousel2-${nuotraukos[0].slice(-13)}" role="button" data-slide="prev">
                                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                        <span class="sr-only">Previous</span>
                                    </a>
                                    <a class="carousel-control-next" href="#carousel2-${nuotraukos[0].slice(-13)}" role="button" data-slide="next">
                                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                        <span class="sr-only">Next</span>
                                    </a>
                                </div>

                            </div>
                        </div>
                    </div>`;
            return template;
    }
    function listTemplate(marke, modelis, metai, nuotraukos, kaina) {
        let template = `
                        <tr>
                            <td id="td1">${marke}</td>
                            <td id="td2">${modelis}</td>
                            <td id="td3">${metai}</td>
                            <td id="td4">${kaina}</td>
                            <td id="td5" class="text-center">`;
                    template += `
                                <button type="button" class="btn btn-sm btn-outline-secondary alert-dark shadow-sm" data-toggle="modal" data-target="#modal-${nuotraukos[0].slice(-13)}">${nuotraukos.length}</button>
                                            `;
                    template += `
                                <!-- Modal -->
                                <div class="modal fade" id="modal-${nuotraukos[0].slice(-13)}" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                    <div class="modal-dialog modal-xl" role="document">
                                        <div class="modal-content">
                                            <div class="modal-header bg-dark p-1 pr-2 d-none">
                                                <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">&times;</span>
                                                </button>
                                            </div>


                                            <div id="carousel2-${nuotraukos[0].slice(-13)}" class="carousel slide" data-ride="carousel">
                                                <ol class="carousel-indicators">`;

                                                nuotraukos.forEach(function(nuotrauka, i) { 
                                                    let active = i == 0 ? 'active' : '';
                                                        template += `
                                                            <li data-target="#carousel2-${nuotraukos[0].slice(-13)}" data-slide-to="${i}" class="${active}"></li>`;
                                                });    

                                    template += `
                                                </ol>
                                                <div class="carousel-inner">`;

                                                    nuotraukos.forEach(function(nuotrauka, i) { 
                                                        let active = i == 0 ? 'active' : '';
                                                        template += `
                                                                    <div class="carousel-item ${active}">
                                                                        <img src="${nuotrauka}" class="d-block w-100">
                                                                    </div>`;
                                                    });

                                    template += `
                                                </div>
                                                <a class="carousel-control-prev" href="#carousel2-${nuotraukos[0].slice(-13)}" role="button" data-slide="prev">
                                                    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                                    <span class="sr-only">Previous</span>
                                                </a>
                                                <a class="carousel-control-next" href="#carousel2-${nuotraukos[0].slice(-13)}" role="button" data-slide="next">
                                                    <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                                    <span class="sr-only">Next</span>
                                                </a>
                                            </div>



                                        </div>
                                    </div>
                                </div>`;
                template += `</td>
                        </tr>`;
        return template;
    }

});
