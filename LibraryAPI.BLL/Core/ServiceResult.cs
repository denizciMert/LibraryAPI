namespace LibraryAPI.BLL.Core
{
    // Generic class to represent the result of a service operation
    public class ServiceResult<T>
    {
#pragma warning disable CS8618 // Null atanamaz alan, oluşturucudan çıkış yaparken null olmayan bir değer içermelidir. Alanı null atanabilir olarak bildirmeyi düşünün.
        public T Data { get; set; } // Property to hold the data returned by the service
#pragma warning restore CS8618 // Null atanamaz alan, oluşturucudan çıkış yaparken null olmayan bir değer içermelidir. Alanı null atanabilir olarak bildirmeyi düşünün.
        public bool Success { get; set; } // Property to indicate if the service operation was successful
#pragma warning disable CS8618 // Null atanamaz alan, oluşturucudan çıkış yaparken null olmayan bir değer içermelidir. Alanı null atanabilir olarak bildirmeyi düşünün.
        public string ErrorMessage { get; set; } // Property to hold the error message in case the service operation failed
#pragma warning restore CS8618 // Null atanamaz alan, oluşturucudan çıkış yaparken null olmayan bir değer içermelidir. Alanı null atanabilir olarak bildirmeyi düşünün.

        // Method to create a successful service result
        public static ServiceResult<T> SuccessResult(T data) => new ServiceResult<T> { Data = data, Success = true };

        // Method to create a failed service result
        public static ServiceResult<T> FailureResult(string errorMessage) => new ServiceResult<T> { Success = false, ErrorMessage = errorMessage };
    }
}