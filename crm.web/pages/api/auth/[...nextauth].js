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
          const user = resData.data
          console.log(user)
          if (resData.code===200) {
            return user
          } else {
            return null
          }
      }
  })
]

const callbacks = {
  async signIn(user, account, profile) {
    if (user) {
      return '/dashboard'
    } else {
      return '/login'
    }
  },
  async jwt(token, user) {
    if (user) {
      token.accessToken = user.token
    }

    return token
  },

  async session(session, token) {
    session.accessToken = token.accessToken
    return session
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
    jwt:true,
    maxAge:1*26*60*60
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