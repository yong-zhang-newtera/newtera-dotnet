﻿/*
 * Newtera .NET Library for Newtera TDM, (C) 2020, 2021 Newtera, Inc.
 *
 */

using Newtera.Exceptions;
using Newtera.Helper;

namespace Newtera.DataModel.Args;

public class GetObjectArgs : ObjectConditionalQueryArgs<GetObjectArgs>
{
    public GetObjectArgs()
    {
        RequestMethod = HttpMethod.Get;
        RequestPath = "/api/blob/objects/";
        OffsetLengthSet = false;
    }

    internal Func<Stream, CancellationToken, Task> CallBack { get; private set; }
    internal long ObjectOffset { get; private set; }
    internal long ObjectLength { get; private set; }
    internal string FileName { get; private set; }
    internal bool OffsetLengthSet { get; set; }

    internal override void Validate()
    {
        base.Validate();
        if (CallBack is null && string.IsNullOrEmpty(FileName))
            throw new NewteraException("Atleast one of " + nameof(CallBack) + ", CallBack method or " + nameof(FileName) +
                                     " file path to save need to be set for GetObject operation.");

        if (OffsetLengthSet)
        {
            if (ObjectOffset < 0)
                throw new InvalidDataException("Offset should be zero or greater: " + nameof(ObjectOffset));

            if (ObjectLength < 0)
                throw new InvalidDataException(
                    "Length should be greater than or equal to zero: " + nameof(ObjectLength));
        }

        if (FileName is not null) Utils.ValidateFile(FileName);
        Populate();
    }

    private void Populate()
    {
        Headers ??= new Dictionary<string, string>(StringComparer.Ordinal);

        if (OffsetLengthSet)
        {
            // "Range" header accepts byte start index and end index
            if (ObjectLength > 0 && ObjectOffset > 0)
                Headers["Range"] = "bytes=" + ObjectOffset + "-" + (ObjectOffset + ObjectLength - 1);
            else if (ObjectLength == 0 && ObjectOffset > 0)
                Headers["Range"] = "bytes=" + ObjectOffset + "-";
            else if (ObjectLength > 0 && ObjectOffset == 0) Headers["Range"] = "bytes=0-" + (ObjectLength - 1);
        }
    }

    internal override HttpRequestMessageBuilder BuildRequest(HttpRequestMessageBuilder requestMessageBuilder)
    {
        requestMessageBuilder.ResponseWriter = CallBack;

        requestMessageBuilder.AddQueryParameter("prefix", Prefix);

        return requestMessageBuilder;
    }

    public GetObjectArgs WithCallbackStream(Action<Stream> cb)
    {
        CallBack = (stream, cancellationToken) =>
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();

            if (cancellationToken.IsCancellationRequested)
                taskCompletionSource.SetCanceled();
            else
                try
                {
                    cb(stream);
                    taskCompletionSource.SetResult(true);
                }
                catch (Exception ex)
                {
                    taskCompletionSource.SetException(ex);
                }

            return taskCompletionSource.Task;
        };
        return this;
    }

    public GetObjectArgs WithCallbackStream(Func<Stream, CancellationToken, Task> cb)
    {
        CallBack = cb;
        return this;
    }

    public GetObjectArgs WithOffsetAndLength(long offset, long length)
    {
        OffsetLengthSet = true;
        ObjectOffset = offset < 0 ? 0 : offset;
        ObjectLength = length < 0 ? 0 : length;
        return this;
    }

    public GetObjectArgs WithLength(long length)
    {
        OffsetLengthSet = true;
        ObjectOffset = 0;
        ObjectLength = length < 0 ? 0 : length;
        return this;
    }

    public GetObjectArgs WithFile(string file)
    {
        FileName = file;
        return this;
    }
}
