<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainForm.aspx.cs" Inherits="VigenereEncoder.MainForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" href="Styles.css" />
    <title>Шифратор Виженера</title></head>
<body class="default-font">
<form runat="server">
    <div class="center-column">
        <h1 class="display-header center-text">Шифратор Виженера</h1>
        <div class="default-block-margin">
            <label>Ключ:</label>
            <input type="text" runat="server" class="text-margin1" id="KeyPattern" name="KeyPattern" placeholder="скорпион"/>
        </div>
        <div class="default-block-margin">
            <label>Ввод:</label>
            <select runat="server" name="InputType" id="InputType" class="select-margin1">
                <option value="UserInput">С клавиатуры</option>
                <option value="Txt">.txt файл</option>
                <option value="Docx">.docx файл</option>
            </select>
            <asp:FileUpload runat="server" class="fileupload-margin" id="InputFile" name="InputFile"/>
        </div>
        <div class="default-block-margin">
            <label>Вывод:</label>
            <select runat="server" name="OutputType" id="OutputType" class="select-margin2">
                <option value="OnScreen">На экран</option>
                <option value="Txt">.txt файл</option>
                <option value="Docx">.docx файл</option>
            </select>
            <input type="text" runat="server" class="text-margin2" id="OutputFilePath" name="OutputFilePath" placeholder="Путь к файлу"/>
        </div>
        <div class="default-block-margin center-text">
            <label runat="server" id="Message" name="Message"></label>
        </div>
        <div class="default-block-margin">
            <textarea runat="server" name="Input" id="Input" class="default-textarea input-position" rows="20" spellcheck="false"></textarea>
            <div class="buttons-position">
                <button runat="server" OnServerClick="EncodeOnClick" name="EncodeButton" id="EncodeButton" type="submit" class="encode-button-style">Зашифровать</button>
                <button runat="server" OnServerClick="DecodeOnClick" name="DecodeButton" id="DecodeButton" type="submit" class="decode-button-style">Расшифровать</button>
            </div>
            <textarea runat="server" name="Output" id="Output" class="default-textarea output-position" rows="20" spellcheck="false" readonly></textarea>
        </div>
    </div>
</form>
</body>
</html>
