﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ЛР1
{
    public enum TokenType
    {
        OpenParenthesisAndStar = 1,
        CloseParenthesisAndStar,

        LeftCurlyBrace,
        RightCurlyBrace,

        DoubleForwardSlash,
        NewLine,

        Text
    }

    public class Tokenn
    {
        private static Dictionary<string, TokenType> DefaultTypes = new Dictionary<string, TokenType>()
    {
        { "(*", TokenType.OpenParenthesisAndStar },
        { "*)", TokenType.CloseParenthesisAndStar },
        { "{", TokenType.LeftCurlyBrace },
        { "}", TokenType.RightCurlyBrace },
        { "//", TokenType.DoubleForwardSlash },
        { "\n", TokenType.NewLine },
    };
        public TokenType Type { get; }
        public string RawToken { get; }
        public int StartPos { get; }
        public int EndPos { get => StartPos + RawToken.Length; }

        public Tokenn(string rawToken, int startPos)
        {
            if (rawToken.Length == 0)
                throw new ArgumentException("raw token is empty");

            RawToken = rawToken;
            StartPos = startPos;
            Type = GetTokenType(rawToken);
        }

        public static bool DefaultTokenExists(string rawToken)
            => DefaultTypes.ContainsKey(rawToken);

        public static TokenType GetTokenType(string rawToken)
        {
            if (DefaultTokenExists(rawToken))
            {
                return DefaultTypes[rawToken];
            }

            return TokenType.Text;
        }
    }
}
