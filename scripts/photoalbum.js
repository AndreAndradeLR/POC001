var msgTimeout = 5000; $(document).ready(function () {
    if (serverVars.adminMode) {
        MakeListEditable(); var status = "Clique no texto para editar e arraste para ordenar as imagens."
        showStatus({ showCloseButton: true, additive: false, afterTimeoutText: status }); showStatus(status);
    }
}); function MakeListEditable() { var list = $("#ulThumbnailList"); list.sortable({ opacity: 0.7, revert: true, scroll: true, handle: $(".imagecontainer") }); $(".photolistitem div[id$=Notes]").makeEditable({ textMode: "multiline", editMode: "html", editClass: "captionedit", updatedColor: "green", editMode: "text" }).css("opacity", 0.8).addClass("captiondisplay"); $("#lblTitle").makeEditable({ editClass: "captionedit", updatedColor: "lightgreen" }); $("#lblDescription").makeEditable({ textMode: "multiline", editClass: "captionedit", updatedColor: "lightgreen", editMode: "formatted" }); $(".photolistitem").hover(function (e) { $(this).find(".deletethumbnail").show().bind("click", DeleteMyPhoto); }, function (e) { $(this).find(".deletethumbnail").hide().unbind("click", DeleteMyPhoto); }); }
function SavePhotos() {
    var items = $(".photolistitem"); var photos = []; for (var x = 0; x < items.length; x++) {
        var photo = {}
        photo.id = items[x].id; photo.notes = textFromHtml($(items[x]).find("div[id$=Notes]").html(), true); photos.push(photo);
    }
    var photoList = {}; photoList.Photos = photos; photoList.Title = $("#lblTitle").text(); photoList.Description = textFromHtml($("#lblDescription").html(), true); httpJson("SaveList", photoList, function (result) { showStatus("Changes have been saved...", msgTimeout); }, pageError); return photos;
}

function DeleteMyPhoto(e) {
    var jItem = $(this).parent();
    var idItem = jItem.find("img[title]").attr("id").replace("Img", "");
    var file = jItem.find("img[title]").attr("title");
    var notes = jItem.find("div[id$=Notes]").text();
    if (!confirm(String.format("Tem certeza que quer deletar\n{0}?\n\n{1}", file, notes)))
        return;
    $("#ctl00_ContentPlaceHolder1_txtHiddenID").val(idItem);
    $("#ctl00_ContentPlaceHolder1_btnDel").click()
}

function DeletePhoto(e) {
    var jItem = $(this).parent();
    var file = jItem.find("img[title]").attr("title");
    var notes = jItem.find("div[id$=Notes]").text();
    if (!confirm(String.format("Are you sure you want to delete\n{0}?\n\n{1}", file, notes)))
        return;
    //    httpJson("DeletePhoto", file, function() {
    //        jItem.remove();
    //        showStatus("Photo has been removed", msgTimeout);
    //    }, pageError);
    alert("Teste para chamar funcao de delete");
}

var progressActive = false;
function ShowImagePopupReal(NomArquivo) {
    window.open("Documentos/" + NomArquivo, "Imagem", "_blank");
}

function ShowImagePopup(hashCode, nextHash, prevHash) {

    var jImageDisplay = $("#imgImage");
    var jImageContainer = $("#ImageContainer");
    var jNotes = $("#" + hashCode + "Notes");
    var jMsg = $("#lblImageMessage");
    var jProgress = $("#imgProgress");
    if ($.browser.msie) {

        jImageContainer.modalDialog({ backgroundOpacity: .65, overlayId: "OpaqueOverlay", zIndex: 100 });
        $("#OpaqueOverlay").click(ClearImagePopup);
        var jImg = $("#" + hashCode + "Img");
        var imgUrl = jImg.attr("src").replace("tb_", "");
        jImageDisplay.attr("src", imgUrl);
        jMsg.text(jNotes.text());

        jImageDisplay.css({ width: "400px", height: "400px" });

        /* Exibe imagens */
        jImageDisplay.bind("load", function () {
            $(this).unbind("load");
            progressActive = false;
            jProgress.hide();
            var ctl = $("#ImageContainer");
            ctl.show();
            jImageDisplay.show();
            var ht = this.height;
            var wd = this.width;
            var wht = $(window).height() - 120;
            if (ht > wht) {
                jImageDisplay.css("height", wht); var newwd = (wht / ht * wd); jImageDisplay.width(newwd); jMsg.width(newwd - 20);
            }
            else jMsg.width(wd - 20);

            $("#OverlayImageHeader").width(this.width + 8);

        });

    } else {

        jImageDisplay.stop().hide();
        jImageContainer.css("opacity", 0.01);
        progressActive = true;
        setTimeout(function () {
            if (progressActive)
                jProgress.attr("src", jProgress.attr("src")).css("zIndex", 400).show().centerInClient();
        }, 200);

        jImageContainer.modalDialog({ backgroundOpacity: .65, overlayId: "OpaqueOverlay", zIndex: 100 });
        $("#OpaqueOverlay").click(ClearImagePopup);
        var jImg = $("#" + hashCode + "Img");
        var imgUrl = jImg.attr("src").replace("tb_", "");
        jImageDisplay.attr("src", imgUrl);
        jMsg.text(jNotes.text());

        jImageDisplay.css({ width: "auto", height: "auto" });

        jImageDisplay.bind("load", function () {
            $(this).unbind("load");
            progressActive = false;
            jProgress.hide();
            var ctl = $("#ImageContainer");
            ctl.show();
            jImageDisplay.show();
            var ht = this.height;
            var wd = this.width;
            var wht = $(window).height() - 120;
            if (ht > wht) {
                jImageDisplay.css("height", wht); var newwd = (wht / ht * wd); jImageDisplay.width(newwd); jMsg.width(newwd - 20);
            }
            else jMsg.width(wd - 20);

            if (nextHash) { $("#WindowNavNext").show().click(function () { httpJson("GetNextPrevPhoto", nextHash, function (photo) { $("#WindowNavNext").unbind("click"); ShowImagePopup(nextHash, photo.Next, photo.Prev); return false; }, null, true); }); }
            else $("#WindowNavNext").hide().unbind("click");

            if (prevHash) { $("#WindowNavPrev").show().click(function (e) { $("#WindowNavPrev").unbind("click"); httpJson("GetNextPrevPhoto", prevHash, function (photo) { ShowImagePopup(prevHash, photo.Next, photo.Prev); return false; }, 100); }); }
            else $("#WindowNavPrev").hide().unbind("click"); ctl.centerInClient(); jImageContainer.css("opacity", 1).hide().fadeIn("slow");
        });
    }
}

function ClearImagePopup() { $("#imgProgress").hide(); $("#ImageContainer").fadeOut("fast", function () { $("#imgImage").attr("src", ""); $("#lblImageMessage").text(""); $(this).modalDialog("hide"); }); }
function SlideShowPlayer() {
    var _I = this; this.photos = []
    this.delay = 5000; this.imageHeight = 400; var curIndex = 0; var playing = false; var cancelPending = false; this.load = function () {
        _I.photos = []; var photos = _I.photos; $(".photolistitem").each(function () {
            var ji = $(this); var photo = {}; var jimg = ji.find("img"); if (jimg.length < 1)
                return; photo.filename = ji.find("img").attr("title"); photo.notes = ji.find("div[id$=Notes]").text(); photos.push(photo);
        });
    }
    this.showPicture = function (index) {
        var jimg = $("#imgSlideShowImage"); var jbox = $("#divSlideShow").css("zIndex", 300); jbox.fadeOut("slow", function () {
            $("#divImageCaption").text(_I.photos[index].notes); jimg.attr("src", _I.photos[index].filename).one("load", function () {
                jbox.centerInClient(); if (playing)
                    _I.nextPicture(); jbox.hide().fadeIn("slow");
            }); if (_I.imageHeight && _I.imageHeight > 0)
                jimg.height(_I.imageHeight);
        });
    }
    this.start = function () { opaqueOverlay({ opacity: .65, zIndex: 100 }); _I.load(); _I.restart(); }
    this.restart = function (next) {
        if (playing)
            return; if (next)
        { curIndex++; if (curIndex > _I.photos.length) curIndex = 0; }
        playing = true; cancelPending = false; _I.showPicture(curIndex);
    }
    this.stop = function ()
    { playing = false; cancelPending = true; }
    this.nextPicture = function () {
        if (playing) {
            setTimeout(function () {
                if (cancelPending || !playing) { cancelPending = false; return; }
                curIndex++; if (curIndex >= _I.photos.length)
                    curIndex = 0; var dispIndex = curIndex; _I.showPicture(dispIndex)
            }, _I.delay);
        } 
    }
    this.next = function () {
        _I.stop(); curIndex++; if (curIndex > _I.photos.length - 1)
            curIndex = 0; cancelPending = true; _I.showPicture(curIndex);
    }
    this.prev = function () {
        _I.stop(); curIndex--; if (curIndex < 0)
            curIndex = _I.photos.length - 1; cancelPending = true; _I.showPicture(curIndex);
    }
    this.hide = function ()
    { $("#divSlideShow").hide(); opaqueOverlay("hide"); }
    this.exit = function ()
    { cancelPending = true; playing = false; _I.hide(); } 
}
var player = new SlideShowPlayer(); player.imageHeight = 450; function Log(message)
{ var text = $("#message").html(); $("#message").html(text + message); }
function UploadWindows(FtpSite, LocalSite) {
    var Win = window.open(FtpSite, "", "Top=0,Left=0,width=400,height=600,resizable=yes,menubar=yes,location=yes"); if (Win == null)
        return; if (LocalSite != null && LocalSite != "")
        window.open(LocalSite, "", "Top=0,Left=410,width=400,height=600,resizable=yes,titlebar=yes,menubar=yes,location=yes")
}
function showAdminDialog()
{ AdminDialog.show(); }
function pageError(xhr, status) {
    var msg = xhr.responseText; if (status == "timeout")
        msg = "Request timed out."
    showStatus(msg, 5000, true);
}
function httpJson(method, data, success, error, isNotAdmin)
{ var json = JSON.stringify(data); $.ajax({ url: serverVars.pageName + "?Callback=" + method + (isNotAdmin ? "" : "&Admin=true;"), data: json, type: "POST", contentType: "application/json", timeout: 10000, success: success, error: error }); }
function textFromHtml(html, fixCR) {
    if (fixCR)
        html = html.replace(/<br.*?>/g, "#CR#"); html = $("<div>" + html + "</div>").text(); if (fixCR)
        html = html.replace(/#CR#/g, "\r\n"); return html;
}
function htmlFromText(text, fixCR)
{ return text.replace(/[\n]/ig, "<br/>"); }