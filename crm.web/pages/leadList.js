import Layout from "../components/layout";
import React, { useState, useEffect, Fragment } from 'react';
import { getSession, useSession } from "next-auth/client";
import { Tab, Dialog, Transition } from '@headlessui/react'

function leadList(data) {

    return(
        <Layout>
            <div className="flex flex-col w-100p h-100p items-center bg-layoutBG p-7 space-y-2">
                
            </div>
        </Layout>
    )
}

export async function getServerSideProps(context) {
    const sess = await getSession(context)
    if (sess) {
      if (process.env.NODE_TLS_REJECT_UNAUTHORIZED !== "0") {
        process.env.NODE_TLS_REJECT_UNAUTHORIZED = "0";
      }
    //   const res = await fetch("https://localhost:44395/api/", {
    //     method: 'GET',
    //     headers: {
    //       accept: '*/*',
    //       "Content-Type": "application/json",
    //       "Authorization": "Bearer " + sess.accessToken
    //     }
    //   })
  
  
    //   const resData = await res.json()
    //   const data = resData.data
  
      return { props: { } }
    } else {
      context.res.writeHead(302, { Location: "/login" })
      context.res.end();
    }
    return null
  }

  export default leadList;