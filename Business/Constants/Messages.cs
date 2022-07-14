using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Product added successfully";
        public static string ProductUpdated = "Product updated successfully";
        public static string ProductDeleted = "Product deleted successfully";

        public static string CategoryAdded = "Category added successfully";
        public static string CategoryUpdated = "Category updated successfully";
        public static string CategoryDeleted = "Category deleted successfully";

        public static string UserNotFound = "User not found!";
        public static string PasswordError = "Invalid password";
        public static string SuccessfulLogin = "Logged in successfully";
        public static string UserAlreadyExists = "User already registered!";
        public static string UserRegistered = "User registered successfully";
        public static string AccessTokenCreated = "Access token created successfully";
        public static string AuthorizationDenied = "You haven't authroity to do that!";

        public static string ProductNameAlreadyExists = "Product name has already exists";
    }
}