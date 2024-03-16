using System.Text.Json.Serialization.Metadata;

namespace Tethr.Sdk.Session
{
    /// <summary>
    /// Manages the connection to the Tethr server and authentication session.
    /// </summary>
    /// <remarks>
    /// Exposed the Raw action for calling the Tethr server, and maintains the authentication token.
    /// This object is thread safe, and should be reused and often is a singleton kept for the lifecycle of the application.
    /// </remarks>
    public interface ITethrSession
    {
        /// <summary>
        /// Clears the current authentication token.
        /// </summary>
        /// <remarks>
        /// This will force the a new Authentication token to be retrieved on the next request.
        /// Often called on the fist <see cref="System.Security.Authentication.AuthenticationException"/>, as it's probable that the token was simply revoked
        /// </remarks>
        void ClearAuthToken();

        /// <summary>
        /// Make a GET request to the server, and return the result
        /// </summary>
        /// <typeparam name="TOut">The object to fill with the result from the server.</typeparam>
        /// <param name="resourcePath">The path to the resource to get.</param>
        /// <param name="outputJsonInfo">The Json Info from JsonSerializerContext for the type that should be returned.</param>
        /// <returns>TOut filled with the result from the server.</returns>
        Task<TOut?> GetAsync<TOut>(string resourcePath, JsonTypeInfo<TOut> outputJsonInfo);

        /// <summary>
        /// Make a POST request to the server, and return the result
        /// </summary>
        /// <typeparam name="TIn">The object type of the body.</typeparam>
        /// <typeparam name="TOut">The object to fill with the result from the server.</typeparam>
        /// <param name="resourcePath">The path to the resource.</param>
        /// <param name="body">The request body to be sent to the server as JSON.</param>
        /// <param name="inputJsonInfo">The Json Info from JsonSerializerContext for the body to send.</param>
        /// <param name="outputJsonInfo">The Json Info from JsonSerializerContext for the type that should be returned.</param>
        /// <returns>TOut filled with the result from the server.</returns>
        Task<TOut> PostAsync<TIn, TOut>(string resourcePath,
            TIn body,
            JsonTypeInfo<TIn> inputJsonInfo,
            JsonTypeInfo<TOut> outputJsonInfo);

        /// <summary>
        /// Make a POST request to the server that doesn't return data.
        /// </summary>
        /// <typeparam name="TIn">The object type of the body.</typeparam>
        /// <param name="resourcePath">The path to the resource.</param>
        /// <param name="body">The request body to be sent to the server as JSON.</param>
        /// <param name="inputJsonInfo">The Json Info from JsonSerializerContext for the body to send.</param>
        Task PostAsync<TIn>(string resourcePath,
            TIn body,
            JsonTypeInfo<TIn> inputJsonInfo);

        /// <summary>
        /// Make a Multi-Part request to the server, using the format required for Tethr to send Audio and MetaData
        /// </summary>
        /// <typeparam name="TIn">The object type of the body.</typeparam>
        /// <typeparam name="TOut">The object to fill with the result from the server.</typeparam>
        /// <param name="resourcePath">The path to the resource to get.</param>
        /// <param name="info">The content to be sterilized and send in the Info part of the request</param>
        /// <param name="inputJsonInfo">The Json Info from JsonSerializerContext for the body to send.</param>
        /// <param name="buffer">The binary data part of the request</param>
        /// <param name="outputJsonInfo">The Json Info from JsonSerializerContext for the type that should be returned.</param>
        /// <param name="dataPartMediaType">The MediaType of the binary data being sent to the server</param>
        /// <returns>TOut filled with the result from the server.</returns>
        Task<TOut> PostMultiPartAsync<TIn, TOut>(string resourcePath, 
            TIn info,
            JsonTypeInfo<TIn> inputJsonInfo,
            Stream buffer,
            JsonTypeInfo<TOut> outputJsonInfo,
            string dataPartMediaType = "application/octet-stream");
    }
}