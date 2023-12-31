﻿namespace test_v1.Bll;

public class BaseApiResponse
{
    public bool IsSucceeded { get; set; }

    public string Message { get; set; }

    private BaseApiResponse(bool isSucceeded, string message)
    {
        IsSucceeded = isSucceeded;
        Message = message;
    }

    public static BaseApiResponse Success(string message = "Ok")
    {
        return new BaseApiResponse(isSucceeded: true, message);
    }

    public static BaseApiResponse Error(string errorMessage)
    {
        return new BaseApiResponse(isSucceeded: false, errorMessage);
    }
}