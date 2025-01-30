using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Zek.Extensions;
using Zek.Model.DTO.Ecomm;
// ReSharper disable InconsistentNaming

namespace Zek.Services
{
    public class EcommService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="rootPath">IHostingEnvironment.ContentRootPath</param>
        public EcommService(EcommOptions options, string rootPath) : this(options.CertificateFile, options.CertificatePassword, options.ServerUrl, options.ClientUrl)
        {
            if (!Path.IsPathRooted(options.CertificateFile))
            {
                _certificateFile = Path.GetFullPath(Path.Combine(rootPath, options.CertificateFile));
            }

        }

        public EcommService(EcommOptions options) : this(options.CertificateFile, options.CertificatePassword, options.ServerUrl, options.ClientUrl)
        {

        }

        public EcommService(string? certificateFile, string certificatePassword, string serverUrl, string? clientUrl = null)
        {
            if (string.IsNullOrEmpty(certificateFile))
                throw new ArgumentException("Certificate file is required", nameof(certificateFile));

            if (string.IsNullOrEmpty(certificatePassword))
                throw new ArgumentException("Certificate password is required", nameof(certificatePassword));

            if (string.IsNullOrEmpty(serverUrl))
                throw new ArgumentException("Server Url is required", nameof(serverUrl));


            //if (string.IsNullOrEmpty(clientUrl))
            //    throw new ArgumentException("Client Url is required", nameof(clientUrl));


            _certificateFile = certificateFile;
            _certificatePassword = certificatePassword;
            _serverUrl = serverUrl;
            _clientUrl = clientUrl;
        }

        private readonly string _serverUrl;
        private readonly string? _clientUrl;
        private readonly string _certificateFile;
        private readonly string _certificatePassword;

        /*      
         
         var sb = new StringBuilder($"command=v&amount={amount:F0}&currency={currency:F0}&client_ip_addr={clientIp}&description={description}&language={language}&msg_type=SMS");
         if (!string.IsNullOrEmpty(merchantTransactionId))
         sb.Append($"&mrch_transaction_id={merchantTransactionId}");
         
        */

        public static bool IsCompleted(EcommResult result)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (result)
            {
                case EcommResult.None:
                case EcommResult.Created:
                case EcommResult.Pending:
                    return false;
            }
            return true;
        }

        private async Task<string> PostAsync(string parameters)
        {
            try
            {
                var clientHandler = new HttpClientHandler();
                var clientCertificate = new X509Certificate2(await File.ReadAllBytesAsync(_certificateFile), _certificatePassword/*, X509KeyStorageFlags.MachineKeySet*/);

                clientHandler.ClientCertificates.Add(clientCertificate);
                clientHandler.ServerCertificateCustomValidationCallback += (message, certificate2, x509Chain, sslPolicyErrors) => true;

#if NET461
            
#endif

#if NETSTANDARD1_6

#endif
                //todo clientHandler.SslProtocols = SslProtocols.Tls11 | SslProtocols.Tls12;
                clientHandler.UseProxy = false;

                using var client = new HttpClient(clientHandler);
                var response = await client.PostAsync(_serverUrl + "?" + parameters, new StringContent(string.Empty));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch// (Exception e)
            {
                throw;
            }
        }

        private static Dictionary<string, string> GetValues(string response)
        {
            var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var l in response.Split(new[] { '\r', '\n', }, StringSplitOptions.RemoveEmptyEntries))
            {
                var line = l.Trim();
                var equalPox = line.IndexOf(':');
                if (equalPox >= 0)
                    values.Add(line.Substring(0, equalPox), line.Substring(equalPox + 2));
            }

            return values;
        }

        private static EcommResponse Deserialize(string response)
        {
            if (string.IsNullOrEmpty(response))
                return null;

            var model = new EcommResponse();
            var values = GetValues(response);

            values.TryGetValue("transaction_id", out var transaction_id);
            model.TransactionId = transaction_id;

            values.TryGetValue("error", out var error);
            model.Error = error;

            values.TryGetValue("result_code", out var result_code);
            model.ResultCode = result_code;

            values.TryGetValue("rrn", out var rrn);
            model.Rrn = rrn;

            values.TryGetValue("approval_code", out var approval_code);
            model.ApprovalCode = approval_code;

            values.TryGetValue("card_number", out var card_number);
            model.CardNumber = card_number;

            values.TryGetValue("result", out var result);
            model.ResultText = result;

            values.TryGetValue("result_ps", out var result_ps);
            model.ResultPaymentServerText = result_ps;

            values.TryGetValue("3dsecure", out var _3dsecure);
            model.Secure3DText = _3dsecure;

            values.TryGetValue("aav", out var aav);
            model.Aav = aav;

            values.TryGetValue("recc_pmnt_id", out var recc_pmnt_id);
            model.RegularPaymentId = recc_pmnt_id;

            values.TryGetValue("recc_pmnt_expiry", out var recc_pmnt_expiry);
            model.RegularPaymentExpiry = recc_pmnt_expiry;

            values.TryGetValue("mrch_transaction_id", out var mrch_transaction_id);
            model.MerchantTransactionId = mrch_transaction_id;

            values.TryGetValue("warning", out var warning);
            model.Warning = warning;

            values.TryGetValue("fld_075", out var fld_075);
            model.Fld075 = fld_075;
            model.CreditReversalCount = fld_075.ToInt32();

            values.TryGetValue("fld_076", out var fld_076);
            model.Fld076 = fld_076;
            model.DebitTransactionCount = fld_076.ToInt32();

            values.TryGetValue("fld_087", out var fld_087);
            model.Fld087 = fld_087;
            model.CreditReversalTotal = fld_087.ToInt32();


            values.TryGetValue("fld_088", out var fld_088);
            model.Fld088 = fld_088;
            model.DebitTransactionTotal = fld_088.ToInt32();

            return model;
        }

        //private static ResultDTO Deserialize(string response)
        //{
        //    if (string.IsNullOrEmpty(response))
        //        return null;

        //    var model = new ResultDTO();
        //    var lines = response.Split(new[] { '\r', '\n', }, StringSplitOptions.RemoveEmptyEntries);
        //    foreach (var l in lines)
        //    {
        //        var line = l.Trim();
        //        var equalPox = line.IndexOf(':');
        //        if (equalPox == -1)
        //            continue;

        //        var key = line.Substring(0, equalPox).ToUpperInvariant();
        //        switch (key)
        //        {
        //            //case "TRANSACTION_ID":
        //            //    model.TransactionId = line.Substring(equalPox + 1).Trim();
        //            //    break;

        //            //case "ERROR":
        //            //    model.Error = line.Substring(equalPox + 1).Trim();
        //            //    break;

        //            //case "RESULT_CODE":
        //            //    model.ResultCode = line.Substring(equalPox + 1).Trim();
        //            //    break;

        //            //case "RRN":
        //            //    model.Rrn = line.Substring(equalPox + 1).Trim();
        //            //    break;

        //            //case "APPROVAL_CODE":
        //            //    model.ApprovalCode = line.Substring(equalPox + 1).Trim();
        //            //    break;

        //            //case "CARD_NUMBER":
        //            //    model.CardNumber = line.Substring(equalPox + 1).Trim();
        //            //    break;

        //            //case "RESULT":
        //            //    model.ResultText = line.Substring(equalPox + 1).Trim();
        //            //    break;

        //            //case "RESULT_PS":
        //            //    model.ResultPaymentServerText = line.Substring(equalPox + 1).Trim();
        //            //    break;

        //            //case "3DSECURE":
        //            //    model.Secure3DText = line.Substring(equalPox + 1).Trim();
        //            //    break;

        //            //case "AAV":
        //            //    model.Aav = line.Substring(equalPox + 1).Trim();
        //            //    break;

        //            //case "RECC_PMNT_ID":
        //            //    model.RegularPaymentId = line.Substring(equalPox + 1).Trim();
        //            //    break;

        //            //case "RECC_PMNT_EXPIRY":
        //            //    model.RegularPaymentExpiry = line.Substring(equalPox + 1).Trim();
        //            //    break;

        //            //case "MRCH_TRANSACTION_ID":
        //            //    model.MerchantTransactionId = line.Substring(equalPox + 1).Trim();
        //            //    break;

        //            //case "WARNING":
        //            //    model.Warning = line.Substring(equalPox + 1).Trim();
        //            //    break;

        //            //case "FLD_075":
        //            //    model.Fld075 = line.Substring(equalPox + 1).Trim();
        //            //    model.CreditReversalCount = model.Fld075.ToInt32();
        //            //    break;

        //            //case "FLD_076":
        //            //    model.Fld076 = line.Substring(equalPox + 1).Trim();
        //            //    model.DebitTransactionCount = model.Fld076.ToInt32();
        //            //    break;

        //            //case "FLD_087":
        //            //    model.Fld087 = line.Substring(equalPox + 1).Trim();
        //            //    model.CreditReversalTotal = model.Fld087.ToInt64();
        //            //    break;

        //            //case "FLD_088":
        //            //    model.Fld088 = line.Substring(equalPox + 1).Trim();
        //            //    model.DebitTransactionTotal = model.Fld088.ToInt32();
        //            //    break;
        //        }
        //    }

        //    return model;
        //}


        /// <summary>
        /// Readdressed url to ECOMM payment server sothat to enter card data.Data is entered using the template provided by the merchant.
        /// </summary>
        /// <param name="transactionId"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public string GetClientRedirectUrl(string transactionId = null)
        {
            if (string.IsNullOrEmpty(_clientUrl))
                throw new ArgumentException("Client Url is required in constructor", nameof(_clientUrl));

            return _clientUrl + (!string.IsNullOrEmpty(transactionId) ? "?trans_id=" + WebUtility.UrlEncode(transactionId) : string.Empty);
        }



        /// <summary>
        /// Registering transactions / Регистрация транзакций
        /// </summary>
        /// <param name="clientIp">Client's IP address, mandatory (15 characters)</param>
        /// <param name="description">Transaction details, optional (up to 125 characters)</param>
        /// <param name="language">Authorization language identifier, optional (up to 32 characters)</param>
        /// <param name="amount">Transaction amount, mandatory</param>
        /// <param name="currency">transaction currency code  (ISO 4217), mandatory</param>
        /// <returns>transaction identifier (28 characters in base64 encoding). In case of an error, the returned string of symbols begins with 'error:'.</returns>
        public Task<TransactionResponse> RegisterTransactionAsync(decimal amount, ISO4217.ISO4217 currency, string clientIp, string description, string language)
            => RegisterTransactionAsync(Convert.ToInt32(amount * 100M), (int)currency, clientIp, description, language);



        //var parameters2 = new Dictionary<string, string>
        //{
        //    ["command"] = "v",
        //    ["amount"] = model.Amount.ToString("F"),
        //    ["currency"] = ((int)model.Currency).ToString("F"),
        //    ["client_ip_addr"] = 
        //};

        /// <summary>
        /// Registering transactions / Регистрация транзакций
        /// </summary>
        /// <param name="clientIp">Client's IP address, mandatory (15 characters)</param>
        /// <param name="description">Transaction details, optional (up to 125 characters)</param>
        /// <param name="language">Authorization language identifier, optional (up to 32 characters)</param>
        /// <param name="amount">Transaction amount in fractional units, mandatory (up to 12 digits)</param>
        /// <param name="currency">transaction currency code  (ISO 4217), mandatory</param>
        /// <returns>transaction identifier (28 characters in base64 encoding). In case of an error, the returned string of symbols begins with 'error:'.</returns>
        public Task<TransactionResponse> RegisterTransactionAsync(int amount, ISO4217.ISO4217 currency, string clientIp, string description, string language)
            => RegisterTransactionAsync(amount, (int)currency, clientIp, description, language);

        /// <summary>
        /// Registering transactions / Регистрация транзакций
        /// </summary>
        /// <param name="clientIp">Client's IP address, mandatory (15 characters)</param>
        /// <param name="description">Transaction details, optional (up to 125 characters)</param>
        /// <param name="language">Authorization language identifier, optional (up to 32 characters)</param>
        /// <param name="amount">Transaction amount in fractional units, mandatory (up to 12 digits)</param>
        /// <param name="currency">transaction currency code  (ISO 4217), mandatory, (3 digits)</param>
        /// <returns>transaction identifier (28 characters in base64 encoding). In case of an error, the returned string of symbols begins with 'error:'.</returns>
        public async Task<TransactionResponse> RegisterTransactionAsync(int amount, int currency, string clientIp, string description, string language)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount parameter is required", nameof(amount));
            if (currency <= 0)
                throw new ArgumentException("Currency parameter is required", nameof(currency));
            if (string.IsNullOrEmpty(clientIp))
                throw new ArgumentException("Client IP parameter is required", nameof(clientIp));
            if (clientIp.Length > 50)
                throw new ArgumentException("Client IP parameter max length is 15", nameof(clientIp));
            //if (string.IsNullOrEmpty(description))
            //    throw new ArgumentException("Description parameter is required", nameof(description));
            if (description != null && description.Length > 125)
                throw new ArgumentException("Description parameter max length is 125", nameof(description));
            if (string.IsNullOrEmpty(language))
                throw new ArgumentException("Language parameter is required", nameof(language));
            if (language.Length > 32)
                throw new ArgumentException("Language parameter max length is 32", nameof(language));

            var response = await PostAsync($"command=v&amount={amount:F0}&currency={currency:F0}&client_ip_addr={clientIp}&desc={description}&language={language}&msg_type=SMS");
            var result = Deserialize(response);

            return new TransactionResponse
            {
                TransactionId = result.TransactionId,

                Error = result.Error,
                Response = response
            };
        }

        /// <summary>
        /// Registering DMS authorization (block amount) / Регистрация DMS авторизации (Прошу учесть, что после этой команды необходимо выполнить процедуру 1.1.3 Transaction result, для выяснения результата.)
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <param name="clientIp"></param>
        /// <param name="description"></param>
        /// <param name="language">Language EN or GE</param>
        /// <returns>transaction identifier (28 characters in base64 encoding). In case of an error, the returned string of symbols begins with ‘error:‘.</returns>
        public Task<TransactionResponse> RegisterDmsAuthorizationAsync(decimal amount, int currency, string clientIp, string description, string language)
            => RegisterDmsAuthorizationAsync(Convert.ToInt32(amount * 100M), currency, clientIp, description, language);

        /// <summary>
        /// Registering DMS authorization (block amount) / Регистрация DMS авторизации (Прошу учесть, что после этой команды необходимо выполнить процедуру 1.1.3 Transaction result, для выяснения результата.)
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <param name="clientIp"></param>
        /// <param name="description"></param>
        /// <param name="language">Language EN or GE</param>
        /// <returns>transaction identifier (28 characters in base64 encoding). In case of an error, the returned string of symbols begins with ‘error:‘.</returns>
        public Task<TransactionResponse> RegisterDmsAuthorizationAsync(decimal amount, ISO4217.ISO4217 currency, string clientIp, string description, string language)
            => RegisterDmsAuthorizationAsync(Convert.ToInt32(amount * 100M), (int)currency, clientIp, description, language);

        /// <summary>
        /// Registering DMS authorization (block amount) / Регистрация DMS авторизации (Прошу учесть, что после этой команды необходимо выполнить процедуру 1.1.3 Transaction result, для выяснения результата.)
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <param name="clientIp"></param>
        /// <param name="description"></param>
        /// <param name="language">Language EN or GE</param>
        /// <returns>transaction identifier (28 characters in base64 encoding). In case of an error, the returned string of symbols begins with ‘error:‘.</returns>
        public Task<TransactionResponse> RegisterDmsAuthorizationAsync(int amount, ISO4217.ISO4217 currency, string clientIp, string description, string language)
            => RegisterDmsAuthorizationAsync(amount, (int)currency, clientIp, description, language);

        /// <summary>
        /// Registering DMS authorization (block amount) / Регистрация DMS авторизации (Прошу учесть, что после этой команды необходимо выполнить процедуру 1.1.3 Transaction result, для выяснения результата.) /
        /// პრე-ავტორიზაცია წარმოადგენს ტრანზაქციის ისეთ ტიპს, სადაც კლიენტს ებლოკება თანხა ბარათზე და არ ეჭრება, სანამ მერჩანტი არ ჩამოაჭრის, ბლოკის შენახვის დრო სტანდარტულად არის 30 დღე, თუმცა შესაძლებელია ზოგიერთ ბანკს სხვა პარამეტრი ჰქონდეს განსაზღვრული, ასე რომ ეს პარამეტრი დამოკიდებულია ბარათის მწარმოებელ ბანკზე (იშუერ ბანკი).
        /// ეს ფუნქცია განსაკუთრებით გამოსადეგი შეიძლება იყოს სასტუმროებისათვის, სადაც მერჩანტმა არ იცის თუ ზუსტად რა თანხა უნდა ჩამოაჭრას კლიენტს ან ნებისმიერი სერვისის გაწევისას როდესაც ანგარიშსწორების თანხა შეიძლება შეიცვალოს.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <param name="clientIp"></param>
        /// <param name="description"></param>
        /// <param name="language">Language EN or GE</param>
        /// <returns>transaction identifier (28 characters in base64 encoding). In case of an error, the returned string of symbols begins with ‘error:‘.</returns>
        public async Task<TransactionResponse> RegisterDmsAuthorizationAsync(int amount, int currency, string clientIp, string description, string language)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount parameter is required", nameof(amount));
            if (currency <= 0)
                throw new ArgumentException("Currency parameter is required", nameof(currency));
            if (string.IsNullOrEmpty(clientIp))
                throw new ArgumentException("Client IP parameter is required", nameof(clientIp));
            if (clientIp.Length > 50)
                throw new ArgumentException("Client IP parameter max length is 15", nameof(clientIp));
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("Description parameter is required", nameof(description));
            if (description.Length > 125)
                throw new ArgumentException("Description parameter max length is 125", nameof(description));
            if (string.IsNullOrEmpty(language))
                throw new ArgumentException("Language parameter is required", nameof(language));
            if (language.Length > 32)
                throw new ArgumentException("Language parameter max length is 32", nameof(language));

            var response = await PostAsync($"command=a&amount={amount:F0}&currency={currency:F0}&client_ip_addr={clientIp}&desc={description}&language={language}&msg_type=DMS");
            var result = Deserialize(response);
            return new TransactionResponse
            {
                TransactionId = result.TransactionId,
                Error = result.Error,
                Response = response
            };
        }


        /// <summary>
        /// პრეავტორიზაციის კომიტი (ბლოკში არსებული თანხის ჩამოსაჭრელად)
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <param name="clientIp"></param>
        /// <param name="description"></param>
        /// <param name="language">Language EN or GE</param>
        /// <returns>
        /// შედეგი:
        /// RESULT: OK
        /// RESULT_CODE: 000
        /// RRN: 123456789012
        /// APPROVAL_CODE: 123456
        /// CARD_NUMBER: 9***********999
        /// </returns>
        public Task<ExecuteDmsTransactionResponse> ExecuteDmsTransactionAsync(string transactionId, decimal amount, ISO4217.ISO4217 currency, string clientIp, string description, string language)
            => ExecuteDmsTransactionAsync(transactionId, Convert.ToInt32(amount * 100M), (int)currency, clientIp, description, language);

        /// <summary>
        /// პრეავტორიზაციის კომიტი (ბლოკში არსებული თანხის ჩამოსაჭრელად)
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <param name="clientIp"></param>
        /// <param name="description"></param>
        /// <param name="language">Language EN or GE</param>
        /// <returns>
        /// შედეგი:
        /// RESULT: OK
        /// RESULT_CODE: 000
        /// RRN: 123456789012
        /// APPROVAL_CODE: 123456
        /// CARD_NUMBER: 9***********999
        /// </returns>
        public Task<ExecuteDmsTransactionResponse> ExecuteDmsTransactionAsync(string transactionId, decimal amount, int currency, string clientIp, string description, string language)
            => ExecuteDmsTransactionAsync(transactionId, Convert.ToInt32(amount * 100M), currency, clientIp, description, language);

        /// <summary>
        /// პრეავტორიზაციის კომიტი (ბლოკში არსებული თანხის ჩამოსაჭრელად)
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <param name="clientIp"></param>
        /// <param name="description"></param>
        /// <param name="language">Language EN or GE</param>
        /// <returns>
        /// შედეგი:
        /// RESULT: OK
        /// RESULT_CODE: 000
        /// RRN: 123456789012
        /// APPROVAL_CODE: 123456
        /// CARD_NUMBER: 9***********999
        /// </returns>
        public Task<ExecuteDmsTransactionResponse> ExecuteDmsTransactionAsync(string transactionId, int amount, ISO4217.ISO4217 currency, string clientIp, string description, string language)
            => ExecuteDmsTransactionAsync(transactionId, amount, (int)currency, clientIp, description, language);

        /// <summary>
        /// პრეავტორიზაციის კომიტი (ბლოკში არსებული თანხის ჩამოსაჭრელად)
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <param name="clientIp"></param>
        /// <param name="description"></param>
        /// <param name="language">Language EN or GE</param>
        /// <returns>
        /// შედეგი:
        /// RESULT: OK
        /// RESULT_CODE: 000
        /// RRN: 123456789012
        /// APPROVAL_CODE: 123456
        /// CARD_NUMBER: 9***********999
        /// </returns>
        public async Task<ExecuteDmsTransactionResponse> ExecuteDmsTransactionAsync(string transactionId, int amount, int currency, string clientIp, string description, string language)
        {
            if (string.IsNullOrEmpty(transactionId))
                throw new ArgumentException("Transaction ID parameter is required", nameof(transactionId));
            if (amount < 0)
                throw new ArgumentException("Amount parameter is required", nameof(amount));
            if (currency <= 0)
                throw new ArgumentException("Currency parameter is required", nameof(currency));
            if (string.IsNullOrEmpty(clientIp))
                throw new ArgumentException("Client IP parameter is required", nameof(clientIp));
            if (clientIp.Length > 50)
                throw new ArgumentException("Client IP parameter max length is 15", nameof(clientIp));
            //if (string.IsNullOrEmpty(description))
            //    throw new ArgumentException("Description parameter is required", nameof(description));
            if (description != null && description.Length > 125)
                throw new ArgumentException("Description parameter max length is 125", nameof(description));
            if (string.IsNullOrEmpty(language))
                throw new ArgumentException("Language parameter is required", nameof(language));
            if (language.Length > 32)
                throw new ArgumentException("Language parameter max length is 32", nameof(language));

            var sb = new StringBuilder($"command=t&trans_id={WebUtility.UrlEncode(transactionId)}&amount={amount:F0}&currency={currency:F0}&client_ip_addr={clientIp}");
            if (!string.IsNullOrEmpty(description))
                sb.Append($"&desc={description}");
            sb.Append($"&language={language}&msg_type=DMS");

            var response = await PostAsync(sb.ToString());
            var result = Deserialize(response);

            return new ExecuteDmsTransactionResponse
            {
                ResultText = result.ResultText,
                Result = result.Result,
                ResultCode = result.ResultCode,
                Rrn = result.Rrn,
                ApprovalCode = result.ApprovalCode,
                CardNumber = result.CardNumber,

                Error = result.Error,
                Response = response
            };
        }


        /// <summary>
        /// Transaction result / Результат транзакции
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="clientIp"></param>
        /// <returns>
        /// RESULT: OK
        /// RESULT_PS: FINISHED
        /// RESULT_CODE: 000
        /// 3DSECURE: ATTEMPTED
        /// RRN: 123456789012
        /// APPROVAL_CODE: 123456
        /// CARD_NUMBER: 9***********9999
        /// RECC_PMNT_ID: 1258
        /// RECC_PMNT_EXPIRY: 1108
        ///  </returns>
        public async Task<GetTransactionResultResponse> GetTransactionResultAsync(string transactionId, string clientIp = null)
        {
            if (string.IsNullOrEmpty(transactionId))
                throw new ArgumentException("Transaction ID parameter is required", nameof(transactionId));

            var sb = new StringBuilder($"command=c&trans_id={WebUtility.UrlEncode(transactionId)}");
            if (!string.IsNullOrEmpty(clientIp))
                sb.Append($"&client_ip_addr={clientIp}");


            var response = await PostAsync(sb.ToString());
            var result = Deserialize(response);

            return new GetTransactionResultResponse
            {
                ResultText = result.ResultText,
                Result = result.Result,
                ResultCode = result.ResultCode,
                ResultPaymentServerText = result.ResultPaymentServerText,
                ResultPaymentServer = result.ResultPaymentServer,
                Secure3DText = result.Secure3DText,
                Secure3D = result.Secure3D,
                Rrn = result.Rrn,
                ApprovalCode = result.ApprovalCode,
                CardNumber = result.CardNumber,
                Aav = result.Aav,
                RegularPaymentId = result.RegularPaymentId,
                RegularPaymentExpiryText = result.RegularPaymentExpiry,
                MerchantTransactionId = result.MerchantTransactionId,

                Error = result.Error,
                Warning = result.Warning,
                Response = response
            };
        }


        /// <summary>
        /// ტრანზაქციის რევერსალი
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public async Task<ReverseResponse> ReverseAsync(string transactionId, int amount = 0)
        {
            if (string.IsNullOrEmpty(transactionId))
                throw new ArgumentException("TransactionId parameter is required", nameof(transactionId));

            var response = await PostAsync($"command=r&trans_id={WebUtility.UrlEncode(transactionId)}&amount={amount:F0}");
            var result = Deserialize(response);

            return new ReverseResponse
            {
                ResultText = result.ResultText,
                Result = result.Result,
                ResultCode = result.ResultCode,

                Error = result.Error,
                Warning = result.Warning,
                Response = response,
            };
        }


        /// <summary>
        /// რეფანდი არის ფინანსურად შესრულებული ოპერაციის თანხის ფინანსურად დაბრუნება, გადარიცხვის საკომისიოს გათვალისწინებით. ძირითადად კლიენტის მოთხოვნით.
        /// Transaction refund / Возврат суммы транзакции
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public async Task<RefundResponse> RefundAsync(string transactionId, int? amount = null)
        {
            if (string.IsNullOrEmpty(transactionId))
                throw new ArgumentException("Transaction ID parameter is required", nameof(transactionId));

            var sb = new StringBuilder($"command=k&trans_id={WebUtility.UrlEncode(transactionId)}");
            if (amount != null)
                sb.Append($"&amount={amount:F0}");

            var response = await PostAsync(sb.ToString());
            var result = Deserialize(response);

            return new RefundResponse
            {
                ResultText = result.ResultText,
                Result = result.Result,
                ResultCode = result.ResultCode,
                RefundTransactionId = result.RefundTransactionId,

                Error = result.Error,
                Warning = result.Warning,
                Response = response
            };
        }


        /// <summary>
        /// Credit transaction / Кредит транзакция
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public async Task<Credit> CreditAsync(string transactionId, int? amount = null)
        {
            if (string.IsNullOrEmpty(transactionId))
                throw new ArgumentException("TransactionId parameter is required", nameof(transactionId));

            var sb = new StringBuilder($"command=g&trans_id={WebUtility.UrlEncode(transactionId)}");
            if (amount != null)
                sb.Append($"&amount={amount:F0}");

            var response = await PostAsync(sb.ToString());
            var result = Deserialize(response);

            return new Credit
            {
                ResultText = result.ResultText,
                Result = result.Result,
                ResultCode = result.ResultCode,
                RefundTransactionId = result.RefundTransactionId,

                Error = result.Error,
                Warning = result.Warning
            };
        }



        /// <summary>
        /// End of business day / Завершение бизнес-дня
        /// </summary>
        /// <returns></returns>
        public async Task<CloseDay> CloseDayAsync()
        {
            var response = await PostAsync("command=b");
            var result = Deserialize(response);

            return new CloseDay
            {
                ResultText = result.ResultText,
                Result = result.Result,
                ResultCode = result.ResultCode,

                Fld075 = result.Fld075,
                CreditReversalCount = result.CreditReversalCount,
                Fld076 = result.Fld076,
                DebitTransactionCount = result.DebitTransactionCount,
                Fld087 = result.Fld087,
                CreditReversalTotal = result.CreditReversalTotal,
                Fld088 = result.Fld088,
                DebitTransactionTotal = result.DebitTransactionTotal
            };
        }


        /// <summary>
        /// Request for SMS transaction/DMS authorization registration
        /// </summary>
        /// <param name="amount">transaction amount in fractional units, mandatory (up to 12 digits)</param>
        /// <param name="currency">transaction currency code (ISO 4217), mandatory, (3 digits)</param>
        /// <param name="clientIp">client’s IP address, mandatory (15 characters)</param>
        /// <param name="description">transaction details (up to 125 characters)</param>
        /// <param name="language"></param>
        /// <param name="regularPaymentId">merchant-selected regular payment identifier</param>
        /// <param name="expiry">preferred deadline for a regular payment MMYY</param>
        /// <param name="overwriteExistingRecurring">If recurring payment for current client (card) is already defined for template, it needs to be overwritten. Overwriting recurring payments can be done by specifying additional parameter perspayee_overwrite=1. In this case all existing recurring payments for template defined for current client (card) will be deleted.</param>
        /// <param name="dms"></param>
        /// <returns></returns>
        public async Task<RegisterRegularPayment> RegisterRegularPaymentAsync(int amount, ISO4217.ISO4217 currency, string clientIp, string description, string language, string regularPaymentId, DateTime expiry, bool overwriteExistingRecurring = false, bool dms = false)
        {
            return await RegisterRegularPaymentAsync(amount, (int)currency, clientIp, description, language, regularPaymentId, expiry, overwriteExistingRecurring, dms);
        }

        /// <summary>
        /// Request for SMS transaction/DMS authorization registration / регистрации регулярного платежа авторизацией с первого платежа:
        /// </summary>
        /// <param name="amount">transaction amount in fractional units, mandatory (up to 12 digits)</param>
        /// <param name="currency">transaction currency code (ISO 4217), mandatory, (3 digits)</param>
        /// <param name="clientIp">client’s IP address, mandatory (15 characters)</param>
        /// <param name="description">transaction details (up to 125 characters)</param>
        /// <param name="language"></param>
        /// <param name="regularPaymentId">merchant-selected regular payment identifier</param>
        /// <param name="expiry">preferred deadline for a regular payment MMYY</param>
        /// <param name="overwriteExistingRecurring">If recurring payment for current client (card) is already defined for template, it needs to be overwritten. Overwriting recurring payments can be done by specifying additional parameter perspayee_overwrite=1. In this case all existing recurring payments for template defined for current client (card) will be deleted.</param>
        /// <param name="dms"></param>
        /// <returns></returns>
        public async Task<RegisterRegularPayment> RegisterRegularPaymentAsync(int amount, int currency, string clientIp, string description, string language, string regularPaymentId, DateTime expiry, bool overwriteExistingRecurring = false, bool dms = false)
        {
            if (amount < 0)
                throw new ArgumentException("Amount parameter is invalid", nameof(amount));
            if (currency <= 0)
                throw new ArgumentException("Currency parameter is required", nameof(currency));
            if (string.IsNullOrEmpty(clientIp))
                throw new ArgumentException("Client IP parameter is required", nameof(clientIp));
            if (clientIp.Length > 50)
                throw new ArgumentException("Client IP parameter max length is 15", nameof(clientIp));
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("Description parameter is required", nameof(description));
            if (description.Length > 125)
                throw new ArgumentException("Description parameter max length is 125", nameof(description));
            if (string.IsNullOrEmpty(language))
                throw new ArgumentException("Language parameter is required", nameof(language));
            if (language.Length > 32)
                throw new ArgumentException("Language parameter max length is 32", nameof(language));
            //if (string.IsNullOrEmpty(regularPaymentId))
            //    throw new ArgumentException("Merchant-selected regular payment identifier parameter is required", nameof(regularPaymentId));



            var command = amount != 0
                ? dms ? "d" : "z"
                : "p";
            var msgType = amount != 0
                ? dms ? "DMS" : "SMS"
                : "AUTH";


            var sb = new StringBuilder($"command={command}");
            if (amount != 0)
                sb.Append($"&amount={amount:F0}");

            sb.Append($"&currency={currency:F0}&client_ip_addr={clientIp}&description={description}&language={language}&msg_type={msgType}&biller_client_id={regularPaymentId}&perspayee_expiry={expiry:MMyy}&perspayee_gen=1");
            if (overwriteExistingRecurring)
                sb.Append("&perspayee_overwrite=1");

            var response = await PostAsync(sb.ToString());
            var result = Deserialize(response);

            return new RegisterRegularPayment
            {
                RegularPaymentId = result.RegularPaymentId,
                TransactionId = result.TransactionId,
                RegularPaymentExpiry = result.RegularPaymentExpiry
            };
        }

        public async Task<ExecuteTransactionResponse> ExecuteRegularPaymentAsync(int amount, ISO4217.ISO4217 currency, string clientIp, string description, string language, string regularPaymentId)
        {
            return await ExecuteRegularPaymentAsync(amount, (int)currency, clientIp, description, language, regularPaymentId);
        }
        public async Task<ExecuteTransactionResponse> ExecuteRegularPaymentAsync(int amount, int currency, string clientIp, string description, string language, string regularPaymentId)
        {
            if (amount < 0)
                throw new ArgumentException("Amount parameter is invalid", nameof(amount));
            if (currency <= 0)
                throw new ArgumentException("Currency parameter is required", nameof(currency));
            if (string.IsNullOrEmpty(clientIp))
                throw new ArgumentException("Client IP parameter is required", nameof(clientIp));
            if (clientIp.Length > 50)
                throw new ArgumentException("Client IP parameter max length is 15", nameof(clientIp));
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("Description parameter is required", nameof(description));
            if (description.Length > 125)
                throw new ArgumentException("Description parameter max length is 125", nameof(description));
            if (string.IsNullOrEmpty(language))
                throw new ArgumentException("Language parameter is required", nameof(language));
            if (language.Length > 32)
                throw new ArgumentException("Language parameter max length is 32", nameof(language));

            var response = await PostAsync($"command=e&amount={amount:F0}&currency={currency:F0}&client_ip_addr={clientIp}&desc={description}&language={language}&biller_client_id={WebUtility.UrlEncode(regularPaymentId)}");
            var result = Deserialize(response);

            return new ExecuteRegularPaymentResponse
            {
                TransactionId = result.TransactionId,

                ResultText = result.ResultText,
                Result = result.Result,
                ResultCode = result.ResultCode,
                Rrn = result.Rrn,
                ApprovalCode = result.ApprovalCode,

                Error = result.Error,
                Response = response
            };
        }
    }
}
