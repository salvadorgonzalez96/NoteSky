using Acr.UserDialogs;
using Newtonsoft.Json;
using NotesSky.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Mail;

namespace NotesSky.Controllers
{
    public static class AppController
    {
        public async static Task<List<Notas>> setRestoreRemindes(string email)
        {
            List<Notas> list = new List<Notas>();
            Repuesta re = new Repuesta();
            using (HttpClient cli = new HttpClient())
            {
                var resp = await cli.GetAsync(Config.Configuration.getListNotas + "?email=" + email);
                if (resp.IsSuccessStatusCode)
                {
                    var contenido = resp.Content.ReadAsStringAsync().Result;
                    re = JsonConvert.DeserializeObject<Repuesta>(contenido);
                    //OBTENER DEL ROOT DEL OBJETO "RESPUESTA" EL ELEMENTO LISTADO que es una lista de alumnos
                    if (re.LISTAD0 != null)
                    {
                        list = re.LISTAD0;
                    }
                    else
                    {

                    }
                    //list = JsonConvert.DeserializeObject<List<Notas>>(contenido);
                }
                else
                {
                    Console.WriteLine(resp);
                }
            }
            return list;
        }
        public async static Task<List<Notas>> setRestoreNote(string email)
        {
            List<Notas> list = new List<Notas>();
            Repuesta re = new Repuesta();
            using (HttpClient cli = new HttpClient())
            {
                var resp = await cli.GetAsync(Config.Configuration.getListNotas + "?email=" + email);
                if (resp.IsSuccessStatusCode)
                {
                    var contenido = resp.Content.ReadAsStringAsync().Result;
                    re = JsonConvert.DeserializeObject<Repuesta>(contenido);
                    //OBTENER DEL ROOT DEL OBJETO "RESPUESTA" EL ELEMENTO LISTADO que es una lista de alumnos
                    if (re.LISTAD0 != null)
                    {
                        list = re.LISTAD0;
                    }
                    else
                    {

                    }
                    //list = JsonConvert.DeserializeObject<List<Notas>>(contenido);
                }
                else
                {
                    Console.WriteLine(resp);
                }
            }
            return list;
        }
        public async static Task<List<Notas>> setFavoritos(string id, string email, string estado)
        {
            List<Notas> list = new List<Notas>();
            Repuesta re = new Repuesta();
            using (HttpClient cli = new HttpClient())
            {
                var resp = await cli.GetAsync(Config.Configuration.getListNotas + "?id=" + id + "?email=" + email + "estado=" + estado);
                if (resp.IsSuccessStatusCode)
                {
                    var contenido = resp.Content.ReadAsStringAsync().Result;
                    re = JsonConvert.DeserializeObject<Repuesta>(contenido);
                    //OBTENER DEL ROOT DEL OBJETO "RESPUESTA" EL ELEMENTO LISTADO que es una lista de alumnos
                    if (re.LISTAD0 != null)
                    {
                        list = re.LISTAD0;
                    }
                    else
                    {

                    }
                    //list = JsonConvert.DeserializeObject<List<Notas>>(contenido);
                }
                else
                {
                    Console.WriteLine(resp);
                }
            }
            return list;
        }
        public async static Task<List<Notas>> getPapeleraReminde(string email)
        {
            List<Notas> list = new List<Notas>();
            Repuesta re = new Repuesta();
            using (HttpClient cli = new HttpClient())
            {
                var resp = await cli.GetAsync(Config.Configuration.getListNotas + "?email=" + email);
                if (resp.IsSuccessStatusCode)
                {
                    var contenido = resp.Content.ReadAsStringAsync().Result;
                    re = JsonConvert.DeserializeObject<Repuesta>(contenido);
                    //OBTENER DEL ROOT DEL OBJETO "RESPUESTA" EL ELEMENTO LISTADO que es una lista de alumnos
                    if (re.LISTAD0 != null)
                    {
                        list = re.LISTAD0;
                    }
                    else
                    {

                    }
                    //list = JsonConvert.DeserializeObject<List<Notas>>(contenido);
                }
                else
                {
                    Console.WriteLine(resp);
                }
            }
            return list;
        }
        public async static Task<List<Notas>> getPapelera(string email)
        {
            List<Notas> list = new List<Notas>();
            Repuesta re = new Repuesta();
            using (HttpClient cli = new HttpClient())
            {
                var resp = await cli.GetAsync(Config.Configuration.getListPapelera + "?email=" + email);
                if (resp.IsSuccessStatusCode)
                {
                    var contenido = resp.Content.ReadAsStringAsync().Result;
                    re = JsonConvert.DeserializeObject<Repuesta>(contenido);
                    //OBTENER DEL ROOT DEL OBJETO "RESPUESTA" EL ELEMENTO LISTADO que es una lista de alumnos
                    if (re.LISTAD0 != null)
                    {
                        list = re.LISTAD0;
                    }
                    else
                    {

                    }
                    //list = JsonConvert.DeserializeObject<List<Notas>>(contenido);
                }
                else
                {
                    Console.WriteLine(resp);
                }
            }
            return list;
        }
        public async static Task<List<Notas>> getFavoritos(string email)
        {
            List<Notas> list = new List<Notas>();
            Repuesta re = new Repuesta();
            using (HttpClient cli = new HttpClient())
            {
                var resp = await cli.GetAsync(Config.Configuration.getListFavv + "?email=" + email);
                if (resp.IsSuccessStatusCode)
                {
                    var contenido = resp.Content.ReadAsStringAsync().Result;
                    re = JsonConvert.DeserializeObject<Repuesta>(contenido);
                    //OBTENER DEL ROOT DEL OBJETO "RESPUESTA" EL ELEMENTO LISTADO que es una lista de alumnos
                    if (re.LISTAD0 != null)
                    {
                        list = re.LISTAD0;
                    }
                    else
                    {

                    }
                    //list = JsonConvert.DeserializeObject<List<Notas>>(contenido);
                }
                else
                {
                    Console.WriteLine(resp);
                }
            }
            return list;
        }
        public async static Task<List<Notas>> getRecordatorios(string email)
        {
            List<Notas> list = new List<Notas>();
            Repuesta re = new Repuesta();
            using (HttpClient cli = new HttpClient())
            {
                var resp = await cli.GetAsync(Config.Configuration.getListRecordatorios + "?email=" + email);
                if (resp.IsSuccessStatusCode)
                {
                    var contenido = resp.Content.ReadAsStringAsync().Result;
                    re = JsonConvert.DeserializeObject<Repuesta>(contenido);
                    //OBTENER DEL ROOT DEL OBJETO "RESPUESTA" EL ELEMENTO LISTADO que es una lista de alumnos
                    if (re.LISTAD0 != null)
                    {
                        list = re.LISTAD0;
                    }
                    else
                    {

                    }
                    //list = JsonConvert.DeserializeObject<List<Notas>>(contenido);
                }
                else
                {
                    Console.WriteLine(resp);
                }
            }
            return list;
        }
        public async static Task<List<Notas>> getNotas(string email) 
        {
            List<Notas> list = new List<Notas>();
            Repuesta re = new Repuesta();
            using (HttpClient cli=new HttpClient())
            {
                var resp = await cli.GetAsync(Config.Configuration.getListNotas+"?email="+email);
                if (resp.IsSuccessStatusCode)
                {
                    var contenido = resp.Content.ReadAsStringAsync().Result;
                    re = JsonConvert.DeserializeObject<Repuesta>(contenido);
                    //OBTENER DEL ROOT DEL OBJETO "RESPUESTA" EL ELEMENTO LISTADO que es una lista de alumnos
                    if (re.LISTAD0 != null)
                    {
                        list = re.LISTAD0;
                    }
                    else
                    {

                    }
                    //list = JsonConvert.DeserializeObject<List<Notas>>(contenido);
                }
                else
                {
                    Console.WriteLine(resp);
                }
            }
            return list;
        }
        public async static Task<List<Notas>> getNotas2(string email,int id)
        {
            List<Notas> list = new List<Notas>();
            Repuesta re = new Repuesta();
            using (HttpClient cli = new HttpClient())
            {
                var resp = await cli.GetAsync(Config.Configuration.getListNotas2 + "?email=" + email +"&id="+id);
                if (resp.IsSuccessStatusCode)
                {
                    var contenido = resp.Content.ReadAsStringAsync().Result;
                    re = JsonConvert.DeserializeObject<Repuesta>(contenido);
                    //OBTENER DEL ROOT DEL OBJETO "RESPUESTA" EL ELEMENTO LISTADO que es una lista de alumnos
                    if (re.LISTAD0 != null)
                    {
                        list = re.LISTAD0;
                    }
                    else
                    {

                    }
                    //list = JsonConvert.DeserializeObject<List<Notas>>(contenido);
                }
                else
                {
                    Console.WriteLine(resp);
                }
            }
            return list;
        }
        public async static Task<contra> getLogin(string txtxUser)
        {
            List<contra> list = new List<contra>();
            Respuesta2 re = new Respuesta2();
            contra vu=new contra();
            using (HttpClient cli = new HttpClient())
            {
                cli.Timeout = TimeSpan.FromSeconds(6);
                var resp = await cli.GetAsync(Config.Configuration.getValidateUser+ "?user="+txtxUser);
                if (resp.IsSuccessStatusCode)
                {
                    var contenido = resp.Content.ReadAsStringAsync().Result;
                    re = JsonConvert.DeserializeObject<Respuesta2>(contenido);
                    //OBTENER DEL ROOT DEL OBJETO "RESPUESTA" EL ELEMENTO LISTADO que es una lista de alumnos
                    list = re.contra;
                    vu = list[0];
                    //list = JsonConvert.DeserializeObject<List<Notas>>(contenido);
                }
            }
            return vu;
        }
        public async static Task<ObjUsuario> getUser(string idUser)
        {
            List<ObjUsuario> list = new List<ObjUsuario>();
            Respuesta3 re = new Respuesta3();
            ObjUsuario vu = new ObjUsuario();
            using (HttpClient cli = new HttpClient())
            {
                var resp = await cli.GetAsync(Config.Configuration.getOnlyUser + "?idUser=" + idUser);
                if (resp.IsSuccessStatusCode)
                {
                    var contenido = resp.Content.ReadAsStringAsync().Result;
                    re = JsonConvert.DeserializeObject<Respuesta3>(contenido);
                    //OBTENER DEL ROOT DEL OBJETO "RESPUESTA" EL ELEMENTO LISTADO que es una lista de alumnos
                    list = re.Usuario;
                    vu = list[0];
                    //list = JsonConvert.DeserializeObject<List<Notas>>(contenido);
                }
            }
            return vu;
        }
        public async static Task<string> setConfig(ObjConfig conf)
        {
            string estado = "";
            String json = JsonConvert.SerializeObject(conf);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                using (HttpClient cli = new HttpClient())
                {
                    var resp = await cli.PostAsync(Config.Configuration.setConfigUser, content);
                    if (resp.IsSuccessStatusCode)
                    {
                        var contenido = resp.Content.ReadAsStringAsync().Result;
                        if (contenido.Contains("0x200"))
                        {
                            if (contenido == "ACTUALIZADO")
                            {
                                estado = "true";
                            }
                            else if (contenido == "CREADO")
                            {
                                estado = "true";
                            }
                        }
                        else
                        {
                            estado = "false";
                        }
                    }
                    else
                    {
                        estado = "false";
                    }
                }
            }
            catch(Exception ex)
            {
                estado = "false";
            }
            
            return estado;
        }
        public async static Task<List<ObjConfig>> getConfig(string idUser)
        {
            List<ObjConfig> list = new List<ObjConfig>();
            Respuesta4 re = new Respuesta4();
            try
            {
                using (HttpClient cli = new HttpClient())
                {
                    var resp = await cli.GetAsync(Config.Configuration.getConfigUser + "?idUser=" + idUser);
                    if (resp.IsSuccessStatusCode)
                    {
                        var contenido = resp.Content.ReadAsStringAsync().Result;
                        re = JsonConvert.DeserializeObject<Respuesta4>(contenido);
                        //OBTENER DEL ROOT DEL OBJETO "RESPUESTA" EL ELEMENTO LISTADO que es una lista de alumnos
                        list = re.LisstConfig;
                        //list = JsonConvert.DeserializeObject<List<Notas>>(contenido);
                    }
                }
            }catch (Exception ex){}
            return list;
        }
        public async static Task<string> setUser(ObjUsuario userobj,String ran)
        {
            string estado = "";
            RespuestaUser re = new RespuestaUser();
            String json = JsonConvert.SerializeObject(userobj);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            using (HttpClient cli = new HttpClient())
            {
                var resp = await cli.PostAsync(Config.Configuration.setnewUser, content);
                if (resp.IsSuccessStatusCode)
                {
                    try
                    {
                        var contenido = resp.Content.ReadAsStringAsync().Result;
                        re = JsonConvert.DeserializeObject<RespuestaUser>(contenido);
                        if (re.message.Contains("Creado") || re.message.Contains("INSERTADO"))
                        {
                            //usuario creado bien enviar el codigo a correo
                            try
                            {
                                MailMessage mail = new MailMessage();
                                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                                mail.From = new MailAddress("noteskyhn@gmail.com");
                                mail.To.Add(userobj.email);
                                mail.Subject = "CODIGO DE VALIDACION DE CUENTA";
                                mail.Body = "CODIGO:" + ran;

                                SmtpServer.Port = 587;
                                SmtpServer.Host = "smtp.gmail.com";
                                SmtpServer.EnableSsl = true;
                                SmtpServer.UseDefaultCredentials = false;
                                SmtpServer.Credentials = new System.Net.NetworkCredential("noteskyhn@gmail.com", "epsmsbbeohnaalba");

                                SmtpServer.Send(mail);
                                return estado = "CORREO ENVIADO";
                            }
                            catch (Exception ex)
                            {
                                return estado = "MAIL fallido";
                            }
                        }
                        else if (re.message == "No Creado")
                        {
                            //Usuario No Creado
                            return estado = "USER no creado";
                        }
                        else if (re.message == "USUARIO EXISTENTE")
                        {
                            //Usuario Ya Existe
                            return estado = "USUARIO EXISTENTE";
                        }
                        else
                        {
                            //Fallo al insertar
                            return estado = "FALLO INSERTAR";
                        }
                    }
                    catch(Exception ex)
                    {
                        return estado = "FALLO INSERTAR";
                    }
                }
                else
                {
                    return estado = "LA PAGINA NO RESPONDIO";
                }
            }
            return estado;
        }
        public async static Task<string> setNewCodigo(ObjUsuario userobj)
        {
            string estado = "";
            RespuestaUser re = new RespuestaUser();
            String json = JsonConvert.SerializeObject(userobj);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            using (HttpClient cli = new HttpClient())
            {
                var resp = await cli.PostAsync(Config.Configuration.setNewCod, content);
                if (resp.IsSuccessStatusCode)
                {
                    var contenido = resp.Content.ReadAsStringAsync().Result;
                    re = JsonConvert.DeserializeObject<RespuestaUser>(contenido);
                    if (re.message=="CODIGO ACTUALIZADO") { return estado = "CODIGO ACTUALIZADO"; }
                    else if (re.message=="CODIGO NO ACTUALIZADO")
                    {
                        return estado = "CODIGO NO ACTUALIZADO";
                    }
                    else if (re.message=="USUARIO NO EXISTE")
                    {
                        return estado = "USUARIO NO EXISTE";
                    }
                    else if(re.message=="USUARIO YA ACTIVADO") 
                    {
                        return estado = "USUARIO YA ACTIVADO";
                    }
                    else
                    {
                        return estado = "FALLO INSERTAR";
                    }
                }
                else
                {
                    return estado = "LA PAGINA NO RESPONDIO";
                }
            }
            return estado;
        }
        public async static Task<string> setVerificaCuenta(string cod)
        {
            string estado = "";
            Object obj = new { codigo = cod };
            RespuestaUser re = new RespuestaUser();
            String json = JsonConvert.SerializeObject(obj);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            using (HttpClient cli = new HttpClient())
            {
                var resp = await cli.PostAsync(Config.Configuration.getverifyCuenta, content);
                if (resp.IsSuccessStatusCode)
                {
                    var contenido = resp.Content.ReadAsStringAsync().Result;
                    re = JsonConvert.DeserializeObject<RespuestaUser>(contenido);
                    if (re.message=="CUENTA ACTIVADA") { return estado = "CUENTA ACTIVADA"; }
                    else if (re.message=="CUENTA NO ACTIVADA")
                    {
                        return estado = "CUENTA NO ACTIVADA";
                    }
                    else if (re.message=="CODIGO INVALIDO O NO EXISTE")
                    {
                        return estado = "CODIGO INVALIDO O NO EXISTE";
                    }
                    else
                    {
                        estado = "CUENTA NO ACTIVADA";
                    }
                }
                else
                {
                    estado = "LA PAGINA NO RESPONDIO";
                }
            }
            return estado;
        }
        public async static Task<string> setNewCodClave(string email,string cod)
        {
            RespuestaUser re = new RespuestaUser();
            string estado = "";
            using (HttpClient cli = new HttpClient())
            {
                cli.Timeout = TimeSpan.FromSeconds(6);
                var resp = await cli.GetAsync(Config.Configuration.setNewCodPassword + "?email=" + email + "&clave=" + cod);
                if (resp.IsSuccessStatusCode)
                {
                    var contenido = resp.Content.ReadAsStringAsync().Result;
                    re = JsonConvert.DeserializeObject<RespuestaUser>(contenido);
                    if (re.message.Contains("ACTUALIZADO") || re.message.Contains("CREADO")) 
                    { 
                        return estado = "ACTUALIZADO"; 
                    }
                    else if (re.message.Contains("NO ACTUALIZADO") || re.message.Contains("NO CREADO"))
                    {
                        return estado = "NO ACTUALIZADO";
                    }
                    else if (re.message.Contains("USUARIO NO EXISTE"))
                    {
                        return estado = "USUARIO NO EXISTE";
                    }
                    else
                    {
                        estado = "INGRESE DATOS PRIMERO!";
                    }
                }
                else
                {
                    estado = "LA PAGINA NO RESPONDIO";
                }
            }
            return estado;
        }
        public async static Task<string> setConfirmCodChangePass(string correo, string codigo_confirm)
        {
            RespuestaUser re = new RespuestaUser();
            string estado = "";
            using (HttpClient cli = new HttpClient())
            {
                cli.Timeout = TimeSpan.FromSeconds(6);
                var resp = await cli.GetAsync(Config.Configuration.setNewCodPassword + "?correo=" + correo + "&codigo_confirm=" + codigo_confirm);
                if (resp.IsSuccessStatusCode)
                {
                    var contenido = resp.Content.ReadAsStringAsync().Result;
                    re = JsonConvert.DeserializeObject<RespuestaUser>(contenido);
                    if (re.message=="CONFIRMADO")
                    {
                        return estado = "CONFIRMADO";
                    }
                    else if (re.message=="NO CONFIRMADO")
                    {
                        return estado = "NO CONFIRMADO";
                    }
                    else
                    {
                        estado = "INGRESE DATOS PRIMERO!";
                    }
                }
                else
                {
                    estado = "LA PAGINA NO RESPONDIO";
                }
            }
            return estado;
        }
        public async static Task<string> setPass(ObjUsuario objUsuario)
        {
            string estado = "";
            RespuestaUser re = new RespuestaUser();
            String json = JsonConvert.SerializeObject(objUsuario);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            using (HttpClient cli = new HttpClient())
            {
                var resp = await cli.PostAsync(Config.Configuration.setChangePass, content);
                if (resp.IsSuccessStatusCode)
                {
                    var contenido = resp.Content.ReadAsStringAsync().Result;
                    re = JsonConvert.DeserializeObject<RespuestaUser>(contenido);
                    if (re.message== "ACTUALIZADO") { return estado = "ACTUALIZADO"; }
                    else if (re.message == "NO ACTUALIZADO")
                    {
                        return estado = "NO ACTUALIZADO";
                    }
                    else if (re.message == "INGRESE UNA CONTRASEÑA")
                    {
                        return estado = "INGRESE UNA CONTRASEÑA";
                    }
                    else if (re.message == "INGRESE UNA CORREO") 
                    {
                        return estado = "INGRESE UNA CORREO";
                    }
                    else
                    {
                        estado = "CONTRASEÑA NO CAMBIADA";
                    }
                }
                else
                {
                    estado = "LA PAGINA NO RESPONDIO";
                }
            }
            return estado;
        }
        public async static Task<string> setUpdateUser(ObjUsuario objUsuario)
        {
            string estado = "";
            RespuestaUser re = new RespuestaUser();
            String json = JsonConvert.SerializeObject(objUsuario);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            using (HttpClient cli = new HttpClient())
            {
                var resp = await cli.PostAsync(Config.Configuration.setUpdateUser, content);
                if (resp.IsSuccessStatusCode)
                {
                    var contenido = resp.Content.ReadAsStringAsync().Result;
                    re = JsonConvert.DeserializeObject<RespuestaUser>(contenido);
                    if (re.message == "ACTUALIZADO") 
                    { 
                        return estado = "ACTUALIZADO"; 
                    }
                    else if (re.message == "NO ACTUALIZADO")
                    {
                        return estado = "NO ACTUALIZADO";
                    }
                    else if (re.message == "1 No ACTUALIZADO")
                    {
                        return estado = "FALLO CORREO";
                    }
                    else if (re.message == "2 No ACTUALIZADO")
                    {
                        return estado = "FALLO NOMBRE";
                    }
                    else if (re.message == "3 No ACTUALIZADO")
                    {
                        return estado = "FALLO APELLIDO";
                    }
                    else if (re.message == "4 No ACTUALIZADO")
                    {
                        return estado = "FALLO TELEFONO";
                    }
                    else if (re.message == "5 No ACTUALIZADO")
                    {
                        return estado = "FALLO CONTRASEÑA";
                    }
                    else if (re.message == "6 No ACTUALIZADO")
                    {
                        return estado = "FALLO CIUDAD";
                    }
                    else if (re.message == "7 No ACTUALIZADO")
                    {
                        return estado = "FALLO IMAGEN";
                    }
                    else
                    {
                        estado = "USUARIO NO ACTUALIZADO";
                    }
                }
                else
                {
                    estado = "LA PAGINA NO RESPONDIO";
                }
            }
            return estado;
        }
        public async static Task<string> setNotaUser(Notas2 objNota)
        {
            string estado = "";
            RespuestaUser re = new RespuestaUser();
            String json = JsonConvert.SerializeObject(objNota);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            using (HttpClient cli = new HttpClient())
            {
                var resp = await cli.PostAsync(Config.Configuration.setNotaUser, content);
                if (resp.IsSuccessStatusCode)
                {
                    var contenido = resp.Content.ReadAsStringAsync().Result;
                    re = JsonConvert.DeserializeObject<RespuestaUser>(contenido);
                    if (re.message == "ENVIADO")
                    {
                        return estado = "ENVIADO";
                    }
                    else if (re.message == "NO ENVIADO")
                    {
                        return estado = "NO ENVIADO";
                    }
                    else
                    {
                        estado = "NO ENVIADO";
                    }
                }
                else
                {
                    estado = "LA PAGINA NO RESPONDIO";
                }
            }
            return estado;
        }
        public async static Task<string> getPdf(string email, int id)
        {
            string respuesta = "";
            using (HttpClient cli = new HttpClient())
            {
                var resp = await cli.GetAsync(Config.Configuration.getPdf64 + "?email=" + email + "&id=" + id);
                if (resp.IsSuccessStatusCode)
                {
                    try
                    {
                        respuesta = resp.Content.ReadAsStringAsync().Result;
                        if (respuesta.Length >= 1)
                        {

                        }
                        else
                        {
                            respuesta = "no obtenido";
                        }
                    }
                    catch (Exception ex)
                    {
                        respuesta = "no obtenido";
                    }
                    //list = JsonConvert.DeserializeObject<List<Notas>>(contenido);
                }
                else
                {
                    respuesta = "no obtenido";
                }
            }
            return respuesta;
        }
        public async static Task<string> getWaves(string email, int id)
        {
            string respuesta = "";
            using (HttpClient cli = new HttpClient())
            {
                var resp = await cli.GetAsync(Config.Configuration.getWavesUser + "?email=" + email + "&id=" + id);
                if (resp.IsSuccessStatusCode)
                {
                    try
                    {
                        respuesta = resp.Content.ReadAsStringAsync().Result;
                        //OBTENER DEL ROOT DEL OBJETO "RESPUESTA" EL ELEMENTO LISTADO que es una lista de alumnos
                        if (respuesta.Length >= 1)
                        {

                        }
                        else
                        {
                            respuesta = "no obtenido";
                        }
                    }catch(Exception ex)
                    {
                        respuesta = "no obtenido";
                    }
                    //list = JsonConvert.DeserializeObject<List<Notas>>(contenido);
                }
                else
                {
                    respuesta = "no obtenido";
                }
            }
            return respuesta;
        }
        public async static Task<string> setRestaurar(string nota, string email,string estado)  
        {
            string respuesta = "";
            using (HttpClient cli = new HttpClient())
            {
                var resp = await cli.GetAsync(Config.Configuration.setNotaRes + "?email=" + email + "&nota=" + nota+"&estado="+estado);
                if (resp.IsSuccessStatusCode)
                {
                    try
                    {
                        respuesta = resp.Content.ReadAsStringAsync().Result;
                        //OBTENER DEL ROOT DEL OBJETO "RESPUESTA" EL ELEMENTO LISTADO que es una lista de alumnos
                        if (respuesta.Contains("ACTUALIZADO"))
                        {
                            respuesta = "ACTUALIZADO";
                        }
                        else
                        {
                            respuesta = "No ACTUALIZADO";
                        }
                    }catch(Exception ex)
                    {
                        respuesta = "No ACTUALIZADO";
                    }
                    //list = JsonConvert.DeserializeObject<List<Notas>>(contenido);
                }
                else
                {
                    respuesta = "No ACTUALIZADO";
                }
            }
            return respuesta;
        }
        public async static Task<string> setRestaurarFav(string nota, string email, string estado)
        {
            string respuesta = "";
            using (HttpClient cli = new HttpClient())
            {
                var resp = await cli.GetAsync(Config.Configuration.setNotaResFav + "?email=" + email + "&nota=" + nota + "&estado=" + estado);
                if (resp.IsSuccessStatusCode)
                {
                    try
                    {
                        respuesta = resp.Content.ReadAsStringAsync().Result;
                        //OBTENER DEL ROOT DEL OBJETO "RESPUESTA" EL ELEMENTO LISTADO que es una lista de alumnos
                        if (respuesta.Contains("ACTUALIZADO"))
                        {
                            respuesta = "ACTUALIZADO";
                        }
                        else
                        {
                            respuesta = "No ACTUALIZADO";
                        }
                    }
                    catch (Exception ex)
                    {
                        respuesta = "No ACTUALIZADO";
                    }
                    //list = JsonConvert.DeserializeObject<List<Notas>>(contenido);
                }
                else
                {
                    respuesta = "No ACTUALIZADO";
                }
            }
            return respuesta;
        }
    }
}
