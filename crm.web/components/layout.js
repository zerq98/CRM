import Sidebar from "./sidebar";
import 'tailwindcss/tailwind.css'

export default function Layout({children}) {

  return (
    <>
      <link href='https://unpkg.com/boxicons@2.0.9/css/boxicons.min.css' rel='stylesheet'></link>
      <div className="w-100p h-fullScreen">
        <Sidebar/>
        <div className="xs:w-80p xl:w-85p h-100p xs:ml-20p xl:ml-15p flex flex-col items-stretch">
          {children}
        </div>
      </div>
    </>
  )
}