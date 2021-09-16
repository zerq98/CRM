import NextAuth from "next-auth";
import { signIn, signOut } from "next-auth/client";
import Providers from "next-auth/providers";

const providers = [
  Providers.Credentials({
    name: 'Credentials',
    authorize: async (credentials) => {
      process.env.NODE_TLS_REJECT_UNAUTHORIZED = "0";
      const res = await fetch("https://localhost:44395/api/Account/Login", {
            method: 'POST',
            body: JSON.stringify(credentials),
            headers: { 
              accept: '*/*',
              "Content-Type": "application/json" 
            }
          })

          const resData = await res.json()
          const token = resData.data
          if (token) {
            return token
          } else {
            return null
          }
      }
  })
]

const callbacks = {
  async signIn() {
    return '/dashboard'
  },
  async jwt(prevToken, token) {
    if (token) {
      return {
        accessToken: token.token,
        accessTokenExpires: Date.now() + 1*24*60*60*1000,
      };
    }
    if (Date.now() < prevToken.accessTokenExpires) {
      return prevToken;
    }

    return null;
  },
  async session(session, token) {
    if(token){
      session.accessToken = token.accessToken
      return session
    }
    return null
  }
}

const options = {
  providers,
  callbacks,
  pages: {
    signIn: '/login',
    error: '/login'
  },
  session:{
    maxAge:1*24*60*60
  },
  events:{
    async signIn(req,res){
      res.writeHead(302,{
        Location: "/dashboard",
      })
      res.end()
    }
  }
}

export default (req, res) => NextAuth(req, res, options)