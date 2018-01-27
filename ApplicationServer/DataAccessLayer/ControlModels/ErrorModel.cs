using System;

namespace JoergIsAGeek.Workshop.DataAccessLayer.ControlModels
{
    /// <summary>
    /// A model that can provide error information to uper levels. It's not going to be stored in database.
    /// </summary>
    public class ErrorModel
    {
        public string Message { get; set; }

        public int Code { get; set; }

        public Exception InnerException { get; set; }

        /// <summary>
        /// The Save result's original value.
        /// </summary>
        /// <returns></returns>
        public int Result { get; internal set; }

        public bool HasException(){
            return InnerException != null;
        }
        
    }
}