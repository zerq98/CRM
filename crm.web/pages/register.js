import 'tailwindcss/tailwind.css'
import { useState } from 'react'
import { register } from './api/account';
import 'react-phone-number-input/style.css'
import Select from 'react-select'

export default function login(){
    const [phone, setPhone] = useState();
    const [province,setProvince] = useState('');
    const provinces=[
        {value:'dolnośląskie', label:'dolnośląskie'},
        {value:'kujawsko-pomorskie', label:'kujawsko-pomorskie'},
        {value:'lubelskie', label:'lubelskie'},
        {value:'lubuskie', label:'lubuskie'},
        {value:'łódzkie', label:'łódzkie'},
        {value:'małopolskie', label:'małopolskie'},
        {value:'mazowieckie', label:'mazowieckie'},
        {value:'opolskie',label:'opolskie'},
        {value:'podkarpackie', label:'podkarpackie'},
        {value:'podlaskie', label:'podlaskie'},
        {value:'pomorskie', label:'pomorskie'},
        {value:'śląskie', label:'śląskie'},
        {value:'świętokrzyskie', label:'świętokrzyskie'},
        {value:'warmińsko-mazurskie', label:'warmińsko-mazurskie'},
        {value:'wielkopolskie', label:'wielkopolskie'},
        {value:'zachodnio-pomorskie', label:'zachodnio-pomorskie'}
    ]

    const handleRegister = function (event) {
        event.preventDefault();
        event.stopPropagation();

        const data ={
            login:event.target.login.value,
            mail:event.target.mail.value,
            password:event.target.password.value,
            firstName:event.target.firstName.value,
            lastName:event.target.lastName.value,
            phone:event.target.phone.value,
            companyName:event.target.companyName.value,
            postCode:event.target.postCode.value,
            city:event.target.city.value,
            street:event.target.address.value,
            houseNo:(event.target.address2.value.split('/'))[0],
            flatNo:(event.target.address2.value.split('/'))[1],
            province:province
        }

        register(data);
    }


    return (
        <div className="bg-loginBG h-screen bg-cover bg-no-repeat bg-center">
            <div className="bg-opacity-015 backdrop-filter backdrop-blur-lg bg-gray md:inset-x-20p inset-y-10p xs:inset-x-10p
            md:w-60p xs:w-80p flex flex-col rounded-3xl relative bg-clip-padding flex flex-col justify-start items-center xs:py-5p md:py-2p">
                <div className="xs:text-sm md:text-xl lg:text-2xl font-bold">No dalej, dołącz do nas!</div>
                <div className="flex flex-col w-75p xs:mx-2p md:mx-5p lg:mx-10p h-50p">
                    <form className="flex flex-col xs:space-y-5 lg:space-y-10 items-center justify-center h-100p w-100p xs:mt-2p lg:mt-5p mx-auto" onSubmit={event => handleRegister(event)}>
                        <div className="w-100p rounded-lg border-2 border-registerBorder flex flex-row flex-wrap justify-between place-content-between p-3 pt-0">
                            <div className="xs:text-xs md:text-lg lg:text-xl w-100p mt-2 text-center">Konto użytkownika</div>
                            <input id="login" name="login" type="text" placeholder="Nazwa użytkownika" required
                            className="shadow-inner w-40p xs:text-sm sm:text-lg md:text-xl xs:h-7 sm:h-8 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5 mt-2"/>
                            <input id="mail" name="mail" type="mail" placeholder="Adres E-mail" required
                            className="shadow-inner w-40p xs:text-sm sm:text-lg md:text-xl xs:h-7 sm:h-8 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5 mt-2"/>
                            <input id="firstName" name="firstName" type="text" placeholder="Imię" required
                            className="shadow-inner w-40p xs:text-sm sm:text-lg md:text-xl xs:h-7 sm:h-8 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5 mt-2"/>
                            <input id="password" name="password" type="password" placeholder="Hasło" required
                            className="shadow-inner w-40p xs:text-sm sm:text-lg md:text-xl xs:h-7 sm:h-8 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5 mt-2"/>
                            <input id="lastName" name="lastName" type="text" placeholder="Nazwisko" required
                            className="shadow-inner w-40p xs:text-sm sm:text-lg md:text-xl xs:h-7 sm:h-8 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5 mt-2"/>
                            <input id="confirm" name="confirm" type="password" placeholder="Potwierdź hasło" required
                            className="shadow-inner w-40p xs:text-sm sm:text-lg md:text-xl xs:h-7 sm:h-8 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5 mt-2"/>
                            <input id="phone" name="phone" type="text" placeholder="Numer telefonu" required
                            className="shadow-inner w-40p xs:text-sm sm:text-lg md:text-xl xs:h-7 sm:h-8 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5 mt-2"/>
                        </div>
                        <div className="w-100p rounded-lg border-2 border-registerBorder flex flex-row flex-wrap justify-between place-content-between p-3 pt-0">
                            <div className="xs:text-xs md:text-lg lg:text-xl w-100p mt-2 text-center">Dane firmy</div>
                            <input id="companyName" name="companyName" type="text" placeholder="Nazwa firmy" required
                            className="shadow-inner w-40p xs:text-sm sm:text-lg md:text-xl xs:h-7 sm:h-8 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5 mt-2"/>
                            <input id="postCode" name="postCode" type="text" placeholder="Kod pocztowy" required
                            className="shadow-inner w-40p xs:text-sm sm:text-lg md:text-xl xs:h-7 sm:h-8 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5 mt-2"/>
                            <input id="city" name="city" type="city" placeholder="Miasto" required
                            className="shadow-inner w-40p xs:text-sm sm:text-lg md:text-xl xs:h-7 sm:h-8 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5 mt-2"/>
                            <input id="address" name="address" type="text" placeholder="Ulica" required
                            className="shadow-inner w-40p xs:text-sm sm:text-lg md:text-xl xs:h-7 sm:h-8 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5 mt-2"/>
                            <Select options={provinces} onChange={value => setProvince(value.value)} placeholder="Województwo" required
                            className="shadow-inner w-40p xs:text-sm sm:text-lg md:text-xl xs:h-7 sm:h-8 md:h-9 text-black rounded-lg focus:outline-none mt-2"/>
                            <input id="address2" name="address2" type="text" placeholder="Nr. domu/mieszkania" required
                            className="shadow-inner w-40p xs:text-sm sm:text-lg md:text-xl xs:h-7 sm:h-8 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5 mt-2"/>
                        </div>
                        
                        <button className='bg-gray rounded-xl hover:bg-white hover:text-gray duration-300 xs:text-sm sm:text-lg md:text-xl font-bold text-white shadow p-2 md:w-70p xs:w-80p' 
                        type='submit'>
                            Zarejestruj firmę
                        </button>
                    </form>
                </div>
            </div>
        </div>
    )
}