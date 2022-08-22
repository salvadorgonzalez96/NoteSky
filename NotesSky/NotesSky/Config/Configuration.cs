using NotesSky.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotesSky.Config
{
    public class Configuration
    {
        //public static String port = "8888";
        public static String port = "8887";

        //public static String ip = "45.182.23.133"+":"+port;
        //public static String ip = "activityoverridenoteskyhn.g4yxq.opdns.net" + ":"+port;
        public static String ip = "192.168.0.10"+":"+port;
        public static String api = "api/notesky";

        public static String ListNotas = "getlistN.php";
        public static String ListFav = "getlistFavorites.php";
        public static String ListNotas2 = "getlistN22.php";
        public static String ListRecordatorios = "getlistR.php";
        public static String ListFavoritos = "getlistF.php";
        public static String ListBasurero = "getlistTrash.php";
        public static String ValidateUser = "consultarusuario.php";
        public static String Onlyuser = "onlyUsuario.php"; 
        public static String ConfigUser = "setConfigbyId.php";
        public static String ListConfigUser = "getConfigbyId.php";
        public static String showPoliticas = "Politicas.html";
        public static String setUser = "crear.php";
        public static String setNewCodigo = "reSendCodigo.php";
        public static String getVerifyAccount = "verifyAccount.php";
        public static String setnewCodPass = "confirmar_codigo.php";
        public static String setnewPass = "restart_pass.php";
        public static String setUpdateU = "Actualizar.php";
        public static String getWaves = "php-waveform-png.php";
        public static String getPdf = "getpdf.php";
        public static String setNotaUsers = "setNewNota.php";
        public static String ListTrash = "getlistTrash.php";
        public static String setNotaRestaura = "setNotaLibre.php";
        public static String setNotaRestaurafv = "setNotaFav.php";

        public static String WebUrlApi = "http://{0}/{1}/{2}";

        //Formar Ruta
        public static String getListNotas = string.Format(WebUrlApi,ip,api,ListNotas); 
        public static String getListNotas2 = string.Format(WebUrlApi,ip,api,ListNotas2); 
        public static String getListRecordatorios = string.Format(WebUrlApi,ip,api,ListRecordatorios); 
        public static String getValidateUser = string.Format(WebUrlApi,ip,api,ValidateUser);
        public static String getOnlyUser = String.Format(WebUrlApi,ip,api,Onlyuser);
        public static String setConfigUser = String.Format(WebUrlApi,ip,api,ConfigUser);
        public static String setnewUser = String.Format(WebUrlApi,ip,api, setUser);
        public static String getConfigUser = String.Format(WebUrlApi,ip,api,ListConfigUser);
        public static String getPagePoliticas = String.Format(WebUrlApi,ip,api,showPoliticas);
        public static String setNewCod = String.Format(WebUrlApi,ip,api, setNewCodigo);
        public static String getverifyCuenta = String.Format(WebUrlApi,ip,api, getVerifyAccount);
        public static String setNewCodPassword = String.Format(WebUrlApi,ip,api, setnewCodPass);
        public static String setChangePass = String.Format(WebUrlApi,ip,api, setnewPass);
        public static String setUpdateUser = String.Format(WebUrlApi,ip,api, setUpdateU);
        public static String getWavesUser = String.Format(WebUrlApi,ip,api, getWaves);
        public static String getPdf64 = String.Format(WebUrlApi,ip,api, getPdf);
        public static String setNotaUser = String.Format(WebUrlApi,ip,api, setNotaUsers);
        public static String getListFavv = String.Format(WebUrlApi,ip,api, ListFav);
        public static String getListPapelera = String.Format(WebUrlApi,ip,api, ListTrash);
        public static String setNotaRes = String.Format(WebUrlApi,ip,api, setNotaRestaura);
        public static String setNotaResFav = String.Format(WebUrlApi,ip,api, setNotaRestaurafv);

        //--CONFIGURACION ESTATICA DEL USUARIO ACTIVO
        public static List<ObjConfig> listC = new List<ObjConfig>();

    }
}
