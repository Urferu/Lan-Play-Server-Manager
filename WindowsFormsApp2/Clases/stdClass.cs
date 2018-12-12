using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Dynamic;
using System.Collections.Generic;

namespace WindowsFormsApp2.Clases
{
    #region Objeto stdClass


    /// <summary>
    /// Son los tipos que se pueden devolver en un indice del objeto stdClassCSharp
    /// </summary>
    public enum TiposDevolver
    {
        /// <summary>
        /// Representa la respuesta en entero, en caso de que la propiedad no cuente con valor asignado
        ///  o se de otro tipo devolverá 0.
        /// </summary>
        Entero,
        /// <summary>
        /// Regresa la respuesta como cadena, en caso de que la propiedad no cuente con valor asignado
        ///  devolverá una cadena vacia.
        /// </summary>
        Cadena,
        /// <summary>
        /// Devuelve el valor como un objeto.
        /// </summary>
        Objeto,
        /// <summary>
        /// Devuelve el control como un stdClassCSharp, en caso de que la propiedad no cuente con valor
        ///  asignado o sea de otro tipo de control devolvera un stdclass vacio
        /// </summary>
        stdClass,
        /// <summary>
        /// Devuelve la respuesta de tipo boleana, en caso de que no sea de tipo boleano devuelve true
        ///  si la propiedad tiene valor asignado y false en caso de que no.
        /// </summary>
        Boleano
    }

    /// <summary>
    /// Clase que representa un objeto json al poder asignarle propiedades en tiempo de ejecución
    /// </summary>
    public class stdClassCSharp : DynamicObject
    {
        #region Obtencion De Clase Dinámica
        private dynamic propiedadesGeneradas;
        private bool esArreglo;

        /// <summary>
        /// Obtiene si el objeto stdClassCSharp es de tipo arreglo
        /// </summary>
        public bool isArrray
        {
            get
            {
                return esArreglo;
            }
        }

        /// <summary>
        /// Obtiene la estructura json que posee el objeto stdClassCSharp actual
        /// </summary>
        public string jsonValue
        {
            get
            {
                return getJsonValue();
            }
        }

        /// <summary>
        /// Obtiene la colección de llaves del objeto stdClassCSharp
        /// </summary>
        public ICollection<string> keysObject
        {
            get
            {
                return ((IDictionary<String, object>)propiedadesGeneradas).Keys;
            }
        }

        /// <summary>
        /// Obtiene el nombre de la llave del indice especificado.
        /// </summary>
        /// <param name="index">Corresponde al indice</param>
        /// <returns>regresa el nombre de la llave.</returns>
        public string getIndexKey(int index)
        {
            return ((IDictionary<String, object>)propiedadesGeneradas).Keys.ElementAt(index);;
        }

        /// <summary>
        /// Devuelve esta misma clase de tipo dynamic para poder acceder a las propiedades 
        /// generadas en la misma de manera directa. <para />
        /// Ejemplo: <para />
        ///     - miClase.nuevaPropiedad = nuevoValor <para />
        ///     - Variable = miClase.nombrePropiedad <para />
        ///   <para />
        ///  Nota: (Ambos objetos comparten la misma memoria por lo que los cambios en uno se 
        ///  ven reflejados directamente en los cambios del otro.<para />
        ///    De esta manera no se cuenta con el apoyo de intellisence solo con un alias).
        /// </summary>
        public dynamic claseDynamicaGenerica
        {
            get
            {
                return this;
            }
        }

        /// <summary>
        /// Establece la propiedad al objeto.
        /// </summary>
        /// <param name="binder">El objeto que contiene la propiedad de bindeo</param>
        /// <param name="value">El valor a asignar</param>
        /// <returns></returns>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (!base.TrySetMember(binder, value))
            {
                var p = propiedadesGeneradas as IDictionary<String, object>;
                p[binder.Name] = value;
            }
            return true;
        }

        /// <summary>
        /// Se encarga de obtener el valor del objeto.
        /// </summary>
        /// <param name="binder">El objeto que contiene la propiedad de bindeo</param>
        /// <param name="result">El valor de respuesta</param>
        /// <returns></returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var p = propiedadesGeneradas as IDictionary<String, object>;
            result = p.ContainsKey(binder.Name) ? p[binder.Name] : null;
            return true;
        }

        #endregion

        /// <summary>
        /// Obtiene o establece el valor del objeto con la llave seleccionada.
        /// </summary>
        /// <param name="key">Corresponde al nombre que se le dio al objeto al generar la llave</param>
        public dynamic this[string key]
        {
            get
            {
                return ((IDictionary<String, object>)propiedadesGeneradas).ContainsKey(key) ?
                    ((IDictionary<String, object>)propiedadesGeneradas)[key] : null;
            }
            set
            {
                var p = propiedadesGeneradas as IDictionary<String, object>;
                p[key.ToString()] = value;
            }
        }

        /// <summary>
        /// Obtiene o establece el valor del objeto con la llave seleccionada. 
        /// </summary>
        /// <param name="key">Corresponde al nombre que se le dio al objeto al generar la llave</param>
        /// <param name="tdDevolver">Es el enumerador que indica que tipo de valor se va a devolver: </para>
        /// Entero devuelve - 0 si el valor es de otro tipo o la llave no existe, </para>
        /// Cadena devuelve - devuelve el valor convertido a string y vacio ("") si la llave
        ///  no existe, </para>
        /// Objeto devuelve - null si es de otro tipo o la llave no existe, </para>
        /// Control devuelve - new Control() si es de otro tipo o la llave no existe. </para>
        /// stdClass devuelve - new stdClassCSharp si es de otro tipo o no existe. </para>
        /// Boleano devuelve - true o false si el objeto tiene o no tiene valor.</param>
        public dynamic this[string key, TiposDevolver tdDevolver]
        {
            get
            {
                return fDevuelveValoresPorTipo(key, tdDevolver);
            }
            set
            {
                var p = propiedadesGeneradas as IDictionary<String, object>;
                p[key.ToString()] = value;
            }
        }

        /// <summary>
        /// Obtiene o establece el valor del objeto con el indice seleccionado.
        /// </summary>
        /// <param name="index">Corresponde al indice del objeto requerido</param>
        public dynamic this[int index]
        {
            get
            {
                string key = ((IDictionary<String, object>)propiedadesGeneradas).Keys.ElementAt(index);

                return ((IDictionary<String, object>)propiedadesGeneradas).ContainsKey(key) ?
                    ((IDictionary<String, object>)propiedadesGeneradas)[key] : "";
            }
            set
            {
                var p = propiedadesGeneradas as IDictionary<String, object>;
                if (((IDictionary<String, object>)propiedadesGeneradas).Keys.Count > index)
                {
                    string key = ((IDictionary<String, object>)propiedadesGeneradas).Keys.ElementAt(index);
                    p[key.ToString()] = value;
                }
                else
                {
                    p[index.ToString()] = value;
                }
            }
        }

        /// <summary>
        /// Obtiene o establece el valor del objeto con el indice seleccionado.
        /// </summary>
        /// <param name="key">Corresponde al indice en caso de que se quiera tomar como algun arreglo</param>
        /// <param name="tdDevolver">Es el enumerador que indica que tipo de valor se va a devolver: 
        /// Entero devuelve - 0 si el valor es de otro tipo o la llave no existe, 
        /// Cadena devuelve - devuelve el valor convertido a string y vacio ("") si la llave no existe, 
        /// Objeto devuelve - null si es de otro tipo o la llave no existe, 
        /// Control devuelve - new Control() si es de otro tipo o la llave no existe.</param>
        public dynamic this[int key, TiposDevolver tdDevolver]
        {
            get
            {
                string llave = ((IDictionary<String, object>)propiedadesGeneradas).Keys.ElementAt(key);
                return fDevuelveValoresPorTipo(llave, tdDevolver);
            }
            set
            {
                var p = propiedadesGeneradas as IDictionary<String, object>;
                if (((IDictionary<String, object>)propiedadesGeneradas).Keys.Count > key)
                {
                    string llave = ((IDictionary<String, object>)propiedadesGeneradas).Keys.ElementAt(key);
                    p[llave.ToString()] = value;
                }
                else
                {
                    p[key.ToString()] = value;
                }
            }
        }

        /// <summary>
        /// Obtiene el valor correspondiente por el tipo de dato requerido
        /// </summary>
        /// <param name="key">La llave que apunta al objeto</param>
        /// <param name="tdDevolver">Corresponde al tipo de dato que se desea devolver</param>
        /// <returns></returns>
        private dynamic fDevuelveValoresPorTipo(string key, TiposDevolver tdDevolver)
        {
            var ValorDevolver = 0;

            if (!((IDictionary<String, object>)propiedadesGeneradas).ContainsKey(key))
            {
                switch (tdDevolver)
                {
                    case TiposDevolver.Entero:
                        return 0;
                    case TiposDevolver.Cadena:
                        return "";
                    case TiposDevolver.Objeto:
                        return null;
                    case TiposDevolver.stdClass:
                        return new stdClassCSharp();
                    case TiposDevolver.Boleano:
                        return false;
                }
            }
            else
            {
                var p = ((IDictionary<String, object>)propiedadesGeneradas);
                switch (tdDevolver)
                {
                    case TiposDevolver.Entero:
                        int iEntero = 0;
                        if (this[key, TiposDevolver.Boleano])
                            int.TryParse(p[key].ToString(), out iEntero);
                        return iEntero;
                    case TiposDevolver.Cadena:
                        try
                        {
                            string sString = "";
                            if (this[key, TiposDevolver.Boleano])
                                sString = p[key].ToString();
                            return sString;
                        }
                        catch
                        {
                            return string.Empty;
                        }
                    case TiposDevolver.Objeto:
                        if (this[key, TiposDevolver.Boleano])
                            return (Object)p[key];
                        else
                            return null;
                    case TiposDevolver.stdClass:
                        if (this[key, TiposDevolver.Boleano] && p[key] is stdClassCSharp)
                            return p[key] as stdClassCSharp;
                        else
                            return new stdClassCSharp();
                    case TiposDevolver.Boleano:
                        if (p[key] == null)
                            return false;
                        else if (p[key] is bool)
                            return (bool)p[key];
                        else
                            return true;
                }
            }

            return ValorDevolver;
        }

        /// <summary>
        /// Crea un objeto de tipo stdClassC#. <para />
        /// Este objeto permite agregar llaves y valores que se necesiten. <para />
        /// Ejemplo: <para />    -Agregar Modificar Valor: objetoClase["nuevaLlave"] = nuevoValor<para />    
        /// -Obtener Valor: variable = objetoClase["llaveObtener"]<para />
        ///  <para />
        /// Nota: (Se recomienda crear una variable nueva con la propiedad claseDynamicaGenerica para otras mejoras.)
        /// </summary>
        /// <param name="vaASerArreglo">Indica si el stdClass que se considera arreglo al momento de generar el jsonValue</param>
        public stdClassCSharp(bool vaASerArreglo = false)
        {
            esArreglo = vaASerArreglo;
            propiedadesGeneradas = new ExpandoObject();
        }

        /// <summary>
        /// Convierte el objeto stdClassCSharp en un arreglo
        /// </summary>
        /// <returns>Un arreglo con los datos del stdClassCSharp</returns>
        public Array toArray()
        {
            var p = ((IDictionary<String, object>)propiedadesGeneradas);
            return p.Values.ToArray();;
        }

        /// <summary>
        /// Agrega un nuevo elemento con el indice númerico correspondiende <para />
        /// al nuevo indice del valor
        /// </summary>
        /// <param name="value">Corresponde al valor a asignar</param>
        public void Add(dynamic value)
        {
            var p = propiedadesGeneradas as IDictionary<String, object>;
            var key = p.Count;
            p[key.ToString()] = value;
        }

        /// <summary>
        /// Obtiene la cantidad de registros contenidas por el objeto stdClassCSharp
        /// </summary>
        public int Count
        {
            get
            {
                return ((IDictionary<String, object>)propiedadesGeneradas).Count;
            }
        }

        /// <summary>
        /// Elimina el objeto con la llave seleccionada.
        /// </summary>
        /// <param name="key">Corresponde al nombre que se le dio al objeto al generar la llave</param>
        public void Remove(string key)
        {
            var p = propiedadesGeneradas as IDictionary<String, object>;
            p.Remove(key);
        }

        /// <summary>
        /// Elimina el objeto con el indice seleccionado.
        /// </summary>
        /// <param name="key">Corresponde al indice del objeto</param>
        public void Remove(int index)
        {
            var p = propiedadesGeneradas as IDictionary<String, object>;
            string llave = p.Keys.ElementAt(index);
            p.Remove(llave);
        }

        /// <summary>
        /// Limpia todas las llaves del objeto general.
        /// </summary>
        public void Clear()
        {
            var p = propiedadesGeneradas as IDictionary<String, object>;
            p.Clear();
        }

        /// <summary>
        /// Crea una copia de memoria del objeto actual
        /// </summary>
        /// <returns>Regresa un stdClassCSharp generado en otro espacio de memoria</returns>
        public stdClassCSharp Clone()
        {
            return stdClassCSharp.jsonToStdClass(this.jsonValue);
        }

        #region generador de strings para server

        /// <summary>
        /// Genera un string en forma de url para enviarse a un get
        /// </summary>
        /// <param name="llavePrincipal">la llave principal del arreglo u objeto</param>
        /// <returns></returns>
        public string getHTTPString(string llavePrincipal = "")
        {
            StringBuilder sbJsonObject = new StringBuilder();
            if (this.Count > 0)
            {
                sbJsonObject.Append(llavePrincipal.Length == 0 ? "?" : "");
                try
                {
                    var p = propiedadesGeneradas as IDictionary<String, object>;
                    foreach (var llave in p.Keys)
                    {
                        var valorActual = p[llave];
                        if (valorActual is stdClassCSharp && !(valorActual as stdClassCSharp).isArrray)
                        {
                            if (llavePrincipal.Length > 0)
                            {
                                sbJsonObject.Append((valorActual as stdClassCSharp).getHTTPString(llavePrincipal + "%5B" + llave + "%5D"));
                            }
                            else
                            {
                                sbJsonObject.Append((valorActual as stdClassCSharp).getHTTPString(llave));
                            }
                        }
                        else if (valorActual is stdClassCSharp && (valorActual as stdClassCSharp).isArrray)
                        {
                            sbJsonObject.Append(convertirListaEnArregloHTTPString(llave, valorActual as stdClassCSharp));
                        }
                        else
                        {
                            if (llavePrincipal.Length > 0)
                            {
                                sbJsonObject.Append(llavePrincipal);
                                sbJsonObject.Append("%5B");
                                sbJsonObject.Append(llave);
                                sbJsonObject.Append("%5D");
                                sbJsonObject.Append("=");
                                sbJsonObject.Append(valorActual.ToString().Replace(' ', '+'));
                            }
                            else
                            {
                                sbJsonObject.Append(llave);
                                sbJsonObject.Append("=");
                                sbJsonObject.Append(valorActual.ToString().Replace(' ', '+'));
                            }
                        }
                        sbJsonObject.Append("&");
                    }
                    sbJsonObject.Remove(sbJsonObject.Length - 1, 1);
                }
                catch
                {
                    sbJsonObject.Clear();
                    sbJsonObject.Append("?");
                }
            }
            return sbJsonObject.ToString();
        }

        /// <summary>
        /// Método que se encarga de convertir un arreglo de json en parte para un HTTPRequest
        /// </summary>
        /// <param name="llave">Correpode al nombre de la llave utilizada</param>
        /// <param name="listaStd">Corresponde a la lista de stdclass</param>
        /// <returns></returns>
        private string convertirListaEnArregloHTTPString(string llave, stdClassCSharp listaStd)
        {
            StringBuilder sbJsonArray = new StringBuilder("");
            try
            {
                int index = 0;
                foreach (stdClassCSharp stdActual in listaStd.toArray())
                {
                    sbJsonArray.Append(stdActual.getHTTPString(llave + "%5B" + index + "%5D"));
                    sbJsonArray.Append("&");
                    index++;
                }
                sbJsonArray.Remove(sbJsonArray.Length - 1, 1);
            }
            catch
            {
                sbJsonArray.Clear();
                sbJsonArray.Append("");
            }
            return sbJsonArray.ToString();
        }

        /// <summary>
        /// Se encarga de obtener el string que representa el json a ser enviado
        /// y el objeto stdClassCSharp actual libera la memoria.
        /// </summary>
        /// <returns></returns>
        public string toJson()
        {
            StringBuilder sbJsonObject = new StringBuilder();
            if (this.isArrray)
            {
                sbJsonObject.Append("[");
            }
            else
            {
                sbJsonObject.Append("{");
            }
            try
            {
                var p = propiedadesGeneradas as IDictionary<String, object>;
                foreach (var llave in p.Keys)
                {
                    var valorActual = p[llave];
                    if (!this.isArrray)
                    {
                        sbJsonObject.Append("\"");
                        sbJsonObject.Append(llave);
                        sbJsonObject.Append("\": ");
                    }
                    if (valorActual is stdClassCSharp)
                    {
                        sbJsonObject.Append((valorActual as stdClassCSharp).toJson());
                        sbJsonObject.Append(",");
                    }
                    else
                    {
                        if (valorActual == null)
                        {
                            sbJsonObject.Append("null");
                            sbJsonObject.Append(",");
                        }
                        else if (valorActual is double || valorActual is int || valorActual is bool)
                        {
                            sbJsonObject.Append(valorActual.ToString().ToLower());
                            sbJsonObject.Append(",");
                        }
                        else
                        {
                            sbJsonObject.Append("\"");
                            sbJsonObject.Append(valorActual);
                            sbJsonObject.Append("\",");
                        }
                    }
                }
                if (sbJsonObject.Length > 1)
                    sbJsonObject.Remove(sbJsonObject.Length - 1, 1);
            }
            catch (Exception ex)
            {
                sbJsonObject.Clear();
                sbJsonObject.Append("{\"error\":\"" + ex.Message + "\"");
            }
            finally
            {
                if (this.isArrray)
                {
                    sbJsonObject.Append("]");
                }
                else
                {
                    sbJsonObject.Append("}");
                }
            }
            return sbJsonObject.ToString();
        }

        #endregion

        #region Actualizar jsonValue

        /// <summary>
        /// Se encarga de actualizar el valor de la propiedad jsonValue
        /// </summary>
        /// <param name="nivel">Corresponde al nivel de tabulaciones en que se encuentra el objeto</param>
        /// <returns></returns>
        public string getJsonValue(int nivel = 0)
        {
            StringBuilder sbJsonObject = new StringBuilder();
            if (this.isArrray)
            {
                sbJsonObject.Append("[");
            }
            else
            {
                sbJsonObject.Append("{");
            }
            try
            {
                var p = propiedadesGeneradas as IDictionary<String, object>;
                foreach (var llave in p.Keys)
                {
                    var valorActual = p[llave];
                    sbJsonObject.Append("\n");
                    agregarTabuladores(sbJsonObject, nivel);

                    sbJsonObject.Append("    ");
                    if (!this.isArrray)
                    {
                        sbJsonObject.Append("\"");
                        sbJsonObject.Append(llave);
                        sbJsonObject.Append("\": ");
                    }
                    if (valorActual is stdClassCSharp)
                    {
                        sbJsonObject.Append((valorActual as stdClassCSharp).getJsonValue(nivel + 1));
                        sbJsonObject.Append(",");
                    }
                    else
                    {
                        if (valorActual == null)
                        {
                            sbJsonObject.Append("null");
                        }
                        else if (valorActual is double || valorActual is int || valorActual is bool)
                        {
                            sbJsonObject.Append(valorActual.ToString().ToLower());
                        }
                        else
                        {
                            sbJsonObject.Append("\"");
                            sbJsonObject.Append(valorActual.ToString().Replace("\n", ". "));
                            sbJsonObject.Append("\"");
                        }

                        sbJsonObject.Append(",");
                    }
                }
                if (sbJsonObject.Length > 1)
                    sbJsonObject.Remove(sbJsonObject.Length - 1, 1);
                sbJsonObject.Append("\n");
                agregarTabuladores(sbJsonObject, nivel);
            }
            catch (Exception ex)
            {
                sbJsonObject.Clear();

                sbJsonObject.Append("{\n    \"error\":\"" + ex.Message + "\"\n");
            }
            finally
            {
                if (this.isArrray)
                {
                    sbJsonObject.Append("]");
                }
                else
                {
                    sbJsonObject.Append("}");
                }
            }
            return sbJsonObject.ToString();
        }

        /// <summary>
        /// Se encarga de agregar las tabulaciones correspondientes al nivel del objeto json
        /// </summary>
        /// <param name="sbATabular">Es el objeto de tipo StringBuilder al cual se le agregaran las tabulaciones</param>
        /// <param name="nivel">indica el nivel de tabulaciones que tendrá</param>
        private void agregarTabuladores(StringBuilder sbATabular, int nivel)
        {
            for (int i = 0; i < nivel; i++)
            {
                sbATabular.Append("    ");
            }
        }

        #endregion

        #region Estáticos

        /// <summary>
        /// Se encarga de generar un archivo stdClass con los datos de los servicios
        /// </summary>
        /// <returns>stdClasCSharp con datos de los servicios rest predeterminados</returns>
        public static stdClassCSharp getFileServices()
        {
            stdClassCSharp archivoAmbiente = new stdClassCSharp();
            if (File.Exists("C:\\SYS\\PROGS\\RESTJSONCONFIG.JSON"))
            {
                StreamReader archivoDeUsuarios = new StreamReader("C:\\SYS\\PROGS\\RESTJSONCONFIG.JSON",
                    Encoding.UTF8, true);
                string textoJson = archivoDeUsuarios.ReadToEnd();
                archivoDeUsuarios.Dispose();
                archivoDeUsuarios.Close();
                stdClassCSharp archivoRest = (stdClassCSharp)JSON.JsonDecode(textoJson);
                archivoAmbiente = archivoRest[archivoRest["ambiente"]];
                archivoAmbiente["ambiente"] = archivoRest["ambiente"];
            }
            return archivoAmbiente;
        }

        /// <summary>
        /// Se encarga de leer un archivo json
        /// </summary>
        /// <param name="fileName">Corresponde a la ruta donde se encuentra el archivo json</param>
        /// <returns>Devuelve un stdClassCSharp con las propiedades del archivo json</returns>
        public static stdClassCSharp readJsonFile(string fileName = "")
        {
            stdClassCSharp fileJson = new stdClassCSharp();
            if (File.Exists(fileName))
            {
                StreamReader archivoJSON = new StreamReader(fileName, Encoding.UTF8, true);
                string textoJson = archivoJSON.ReadToEnd();
                archivoJSON.Dispose();
                archivoJSON.Close();
                fileJson = (stdClassCSharp)JSON.JsonDecode(textoJson);
            }
            return fileJson;
        }

        /// <summary>
        /// Se encarga de leer un archivo json
        /// </summary>
        /// <param name="fileName">Corresponde a la ruta donde se encuentra el archivo json</param>
        /// <returns>Devuelve un stdClassCSharp con las propiedades del archivo json</returns>
        public void writeJsonFile(string fileName = "")
        {
            StreamWriter archivoJSON = new StreamWriter(fileName, false, Encoding.UTF8);
            archivoJSON.Write(this.jsonValue);
            archivoJSON.Close();
        }

        /// <summary>
        /// Se encarga de convertir un json string de respuesta del servidor a un stdClassCSharp
        /// </summary>
        /// <param name="json">Representa al json a convertir</param>
        /// <returns></returns>
        public static stdClassCSharp jsonToStdClass(string json)
        {
            return (stdClassCSharp)JSON.JsonDecode(json);
        }

        #endregion
    }

    #endregion
}
