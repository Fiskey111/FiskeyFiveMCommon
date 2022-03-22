﻿using static CitizenFX.Core.Native.API;
using System;
using CitizenFX.Core.Native;
using System.Drawing;
using CitizenFX.Core;

namespace CommonClient.UI
{
    public class Scaleform
    {
        public const int InvalidHandle = 0;

        public int Handle { get; private set; } = InvalidHandle;
        public bool IsLoaded => Handle != InvalidHandle && HasScaleformMovieLoaded(Handle);

        public Scaleform() { }

        public Scaleform(int handle) => Handle = handle;

        public bool Load(string scaleformID)
        {
            Handle = RequestScaleformMovie(scaleformID);

            return Handle != InvalidHandle;
        }


        public void Render2D()
        {
            const ulong DrawScaleformMovieDefault = 0x0df606929c105be1;
            Function.Call((Hash)DrawScaleformMovieDefault, Handle, 255, 255, 255, 255);
        }
        public void Render2DScreenSpace(PointF location, PointF size)
        {
            float x = location.X / 1280.0f;
            float y = location.Y / 720.0f;
            float width = size.X / 1280.0f;
            float height = size.Y / 720.0f;

            DrawScaleformMovie(Handle, x + width / 2.0f, y + height / 2.0f, width, height, 255, 255, 255, 255, 0);
        }


        public void Render3D(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            Function.Call<uint>((Hash)0x1ce592fdc749d6f5, Handle, position.X, position.Y, position.Z, rotation.X, rotation.Y, rotation.Z, 2.0f, 2.0f, 1.0f, scale.X, scale.Y, scale.Z, 2);
        }
        public void Render3DAdditive(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            Function.Call<uint>((Hash)0x87d51d72255d4e78, Handle, position.X, position.Y, position.Z, rotation.X, rotation.Y, rotation.Z, 2.0f, 2.0f, 1.0f, scale.X, scale.Y, scale.Z, 2);
        }


        public void CallFunction(string function, params object[] arguments)
        {
            const ulong PushScaleformMovieFunctionHash = 0xf6e48914c7a8694e;
            const ulong PushScaleformMovieFunctionParameterIntHash = 0xc3d0841a0cc546a6;
            const ulong BeginTextComponentHash = 0x80338406f3475e55;
            const ulong AddTextComponentStringHash = 0x6c188be134e074aa;
            const ulong EndTextComponentHash = 0x362e2d3fe93a9959;
            const ulong PushScaleformMovieFunctionParameterFloatHash = 0xd69736aae04db51a;
            const ulong PushScaleformMovieFunctionParameterBoolHash = 0xc58424ba936eb458;
            const ulong PushScaleformMovieFunctionParameterStringHash = 0xba7148484bd90365;
            const ulong PopScaleformMovieFunctionVoidHash = 0xc6796a8ffa375e53;


            Function.Call<uint>((Hash)PushScaleformMovieFunctionHash, Handle, function);

            foreach (object obj in arguments)
            {
                if (obj.GetType() == typeof(int))
                {
                    Function.Call<uint>((Hash)PushScaleformMovieFunctionParameterIntHash, (int)obj);
                }
                else if (obj.GetType() == typeof(string))
                {
                    Function.Call<uint>((Hash)BeginTextComponentHash, "STRING");
                    Function.Call<uint>((Hash)AddTextComponentStringHash, (string)obj);
                    Function.Call<uint>((Hash)EndTextComponentHash);
                }
                else if (obj.GetType() == typeof(char))
                {
                    Function.Call<uint>((Hash)BeginTextComponentHash, "STRING");
                    Function.Call<uint>((Hash)AddTextComponentStringHash, ((char)obj).ToString());
                    Function.Call<uint>((Hash)EndTextComponentHash);
                }
                else if (obj.GetType() == typeof(float))
                {
                    Function.Call<uint>((Hash)PushScaleformMovieFunctionParameterFloatHash, (float)obj);
                }
                else if (obj.GetType() == typeof(double))
                {
                    Function.Call<uint>((Hash)PushScaleformMovieFunctionParameterFloatHash, (float)(double)obj);
                }
                else if (obj.GetType() == typeof(bool))
                {
                    Function.Call<uint>((Hash)PushScaleformMovieFunctionParameterBoolHash, (bool)obj);
                }
                else if (obj.GetType() == typeof(ScaleformArgumentTXD))
                {
                    Function.Call<uint>((Hash)PushScaleformMovieFunctionParameterStringHash, ((ScaleformArgumentTXD)obj).TXD);
                }
                else
                {
                    Debug.WriteLine(string.Format("Unknown argument type {0} passed to scaleform with handle {1}.", obj.GetType().Name, Handle));
                }
            }

            Function.Call<uint>((Hash)PopScaleformMovieFunctionVoidHash);
        }
    }

    public class ScaleformArgumentTXD
    {
        private string _txd;
        public string TXD { get { return _txd; } }

        public ScaleformArgumentTXD(string txd)
        {
            _txd = txd;
        }
    }
}
