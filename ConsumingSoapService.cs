
HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://urlserviciosoap/test/clientesv1.0");
request.Headers.Add(@"SOAP:Action");
request.ContentType = "text/xml;charset=\"utf-8\"";
request.Accept = "text/xml";
request.Method = "POST";

XmlDocument SOAPReqBody = new XmlDocument();
SOAPReqBody.LoadXml(
    string.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:cli=""http://urlserviciosoap/common/schema/clientservice"" xmlns:cli1=""http://urlserviciosoap/common/schema/clientservice"" xmlns:req=""http://urlserviciosoap.com/common/schema/Cliente/Req-v2018.02"">
<soapenv:Header>
      <cli:ClientService>
         <cli:brand>{0}</cli:brand>
         <cli:user>{1}</cli:user>
      </cli:ClientService>
</soapenv:Header>
<soapenv:Body>
      <req:clienteConsultarReq>
         <Entidad1>
            <idContract>{2}</idContract>
         </Entidad1>
      </req:clienteConsultarReq>
   </soapenv:Body>
</soapenv:Envelope>", objClient.brand, objClient.user, objClientService.contractId));

                using (Stream stream = request.GetRequestStream())
                {
                    SOAPReqBody.Save(stream);
                }

                using (WebResponse Serviceres = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
                    {
                        var ServiceResult = rd.ReadToEnd();

                        if (!string.IsNullOrEmpty(ServiceResult.ToString()))
                        {
                            try
                            {
                                XmlDocument res = new XmlDocument();
                                res.LoadXml(ServiceResult.ToString());

                                if (res != null)
                                {
                                    if (res.DocumentElement != null && res.DocumentElement.ChildNodes[0] != null &&
                                    res.DocumentElement.ChildNodes[0].ChildNodes[0] != null &&
                                        res.DocumentElement.ChildNodes[0].ChildNodes[0].ChildNodes[0] != null &&
                                        !string.IsNullOrEmpty(res.DocumentElement.ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[0].InnerText))
                                    {
                                        objRespuesta.Code = res.DocumentElement.ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[0].InnerText;//campo codigo de la respuesta del xml
                                        objRespuesta.Message = res.DocumentElement.ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[1].InnerText;//campo mensaje de la respuesta del xml
                                    }
                                  }
                               }
                               catch(exception ex){
                               
                               
                               }
                               
                            }
                        }
                        
                     }
