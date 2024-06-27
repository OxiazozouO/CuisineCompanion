using System;
using System.Windows;
using CuisineCompanion.Helper;
using CuisineCompanion.WebApi;
using Newtonsoft.Json;
using RestSharp;
using static System.Net.HttpStatusCode;

namespace CuisineCompanion.HttpClients;

public static class HttpRestClient
{
    private const string BaseUrl = "http://localhost:5281/api/";
    private static readonly RestClient _client = new(BaseUrl);

    public static bool Execute(this ApiRequest apiRequest, bool isShowError = false) =>
        Execute(apiRequest, out _, isShowError);

    public static bool Execute(this ApiRequest apiRequest, out ApiResponses ret, bool isShowError = false)
    {
        var request = new RestRequest(apiRequest.Url, apiRequest.Method);
        request.AddHeader("Content-Type", apiRequest.ContentType);
        if (apiRequest.Parameters is not null)
        {
            var json = JsonConvert.SerializeObject(apiRequest.Parameters);
            request.AddParameter("application/json", json, ParameterType.RequestBody);
        }

        var res = _client.Execute(request);
        ret = res.StatusCode switch
        {
            OK => res.Content.ToEntity<ApiResponses>(),
            _ => ApiResponses.Error(res.StatusCode switch
            {
                Continue => "服务器正在处理请求，请稍候再试",
                SwitchingProtocols => "服务器正在处理请求，请稍候再试",
                Processing => "服务器正在处理请求，请稍候再试",
                EarlyHints => "服务器正在处理请求，请稍候再试",
                Created => "请求已被接受但尚未完成处理",
                Accepted => "请求已被接受但尚未完成处理",
                NonAuthoritativeInformation => "返回的信息可能不是最新的",
                BadRequest => "客户端请求错误",
                Unauthorized => "未授权的请求",
                InternalServerError => "服务器内部错误",
                NotImplemented => "请求的功能尚未实现",
                BadGateway => "网关错误",
                _ => "服务器忙"
            }),
        };

        if (ret.Code == 0 && isShowError)
        {
            MsgBoxHelper.TryError(ret.Message);
        }

        return ret.Code == 1;
    }


    public static bool FileUpload(string filePath, out string outFileName)
    {
        outFileName = "";
        if (!FileViewHelper.FileCheck(filePath)) return false;


        var request = new RestRequest("File/Upload", Method.POST);
        request.AddHeader("Content-Type", "multipart/form-data");
        try
        {
            request.AddFile("file", filePath);

            var res = _client.Execute(request);

            if (res.StatusCode == OK)
            {
                outFileName = res.Content.ToEntity<ApiResponses>().Data?.ToString() ?? "";
                if (string.IsNullOrEmpty(outFileName))
                {
                    MsgBoxHelper.TryError($"上传失败");
                    return false;
                }

                return true;
            }

            MsgBoxHelper.TryError($"上传失败：{res.ErrorMessage}");
            return false;
        }
        catch (Exception ex)
        {
            MsgBoxHelper.TryError($"发生错误：{ex.Message}");
            return false;
        }
    }
}