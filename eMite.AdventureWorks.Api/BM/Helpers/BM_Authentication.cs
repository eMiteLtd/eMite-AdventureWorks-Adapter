using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bcl = eMite.Framework.Bcl;

namespace eMite.AdventureWorks.Api.BM.Helpers
{
    public class BM_Authentication: bcl.Base.BM.BM_NoCrudBase
    {
        #region "Properties"
        public static string AuthenticatedToken { get; set; }
        public static bool IsAuthenticated { get; set; }

        private string AuthenticatedTokenKey = "AuthenticatedToken";
        #endregion

        #region "Constructors"
        public BM_Authentication()
        {
            AuthenticatedToken = "";

            Authenticate();
        }
        #endregion

        #region "Public Methods"

        /// <summary>
        /// Authenticate method can be called to perform the authentication against the source
        /// </summary>
        /// <returns></returns>
        public bool Authenticate()
        {
            IsAuthenticated = GetToken();

            return IsAuthenticated;
        }

        #endregion

        #region "Private Methods"

        /// <summary>
        /// GetToken is a sample method to show how the login/authentication to the source system can be performed and a AuthenticationToken can be saved to the eMite database via the Configurations api
        /// </summary>
        /// <returns></returns>
        private bool GetToken()
        {
            bool IsTokenValid = false;
            

            if (string.IsNullOrEmpty(AuthenticatedToken))
            {
                var AuthenticatedTokenKV = Misc.Utilities.BmAdapterData.GetData(AuthenticatedTokenKey);

                if (AuthenticatedTokenKV == null)
                {
                    //do login and set the authentication token 
                    IsTokenValid = Login();

                }
                else
                {
                    //key does exist in the database so we assign it back to the variable. Static variable in this example.
                    AuthenticatedToken = AuthenticatedTokenKV.ValueString;
                }

            }


            #region "Validate if the token is valid"

            
            //Validate if the token is valid              
            IsTokenValid = ValidateToken();

            #endregion


            return IsTokenValid;
        }

        /// <summary>
        /// Sample method to validate the available token.
        /// </summary>
        /// <returns></returns>
        private bool ValidateToken()
        {
            bool IsTokenValid = false;
                        
            if(!string.IsNullOrEmpty(AuthenticatedToken))
            {
                //Write you code to validate token here.
                IsTokenValid = true;
                IsAuthenticated = true;
            }
            else
            {
                IsAuthenticated = false;
            }

            return IsTokenValid;

        }

        /// <summary>
        /// Sample method to perform the login/authentication process with the source
        /// </summary>
        /// <returns></returns>
        private bool Login()
        {
            bool IsLoginSuccessful = false;

            //###### Write you code to authenticate/login to the source system here ######
            IsLoginSuccessful = true;

            if (IsLoginSuccessful)
            {
                AuthenticatedToken = "qwertyuiop1234567890";

                //Since the adpater data (AuthenticatedToken) did not exist in the database. We add the key and value for saving
                Misc.Utilities.BmAdapterData.AddData(AuthenticatedTokenKey, AuthenticatedToken, bcl.Configurations.BM.BM_Configurations.ValueType.String);

                //save to database.
                Misc.Utilities.BmAdapterData.Save();

                IsAuthenticated = true;

            }
            else
            {
                //if login was not successful then we set the authenticated token to empty.
                AuthenticatedToken = "";
                IsAuthenticated = false;
            }

            return IsLoginSuccessful;

        }

        #endregion

    }
}
