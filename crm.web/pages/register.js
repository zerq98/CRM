import 'tailwindcss/tailwind.css'
import { useRouter } from 'next/router'
import {server} from './config'

export default function login() {

    const router = useRouter()
    const handleRegister = async function (event) {
        alert('Trwa tworzenie konta, to może potrwać chwilę.')
        event.preventDefault();
        event.stopPropagation();

        const data = {
            user: {
                login: event.target.login.value,
                email: event.target.mail.value,
                password: event.target.password.value,
                firstName: event.target.firstName.value,
                lastName: event.target.lastName.value,
                phoneNumber: event.target.phone.value
            },
            companyAddress: {
                postCode: event.target.postCode.value,
                city: event.target.city.value,
                street: event.target.address.value,
                houseNumber: (event.target.address2.value.split('/'))[0],
                apartmentNumber: (event.target.address2.value.split('/'))[1],
                province: event.target.province.value
            },
            userAddress: {
                postCode: '',
                city: '',
                street: '',
                houseNumber: '',
                apartmentNumber: '',
                province: ''
            },
            company: {
                companyName: event.target.companyName.value
            }
        }

        if (validate(data, event.target.confirm.value)) {
            const res = await fetch(server+"Account/RegisterCompany", {
                method: 'POST',
                body: JSON.stringify(data),
                headers: {
                    accept: '*/*',
                    "Content-Type": "application/json"
                }
            })

            const resData = await res.json()

            if (resData.code === 201) {
                router.push('/login')
            } else {
                alert(resData.errorMessage)
                return 'Wrong data';
            }
        }
        event.target.login.value = '';
        event.target.mail.value = '';
        event.target.password.value = '';
        event.target.confirm.value='';
        event.target.firstName.value = '';
        event.target.lastName.value = '';
        event.target.phone.value = '';
        event.target.companyName.value = '';
        event.target.postCode.value = '';
        event.target.city.value = '';
        event.target.address.value = '';
        event.target.address2.value = '';
        event.target.province.value = '';
    }


    return (
        <div className="bg-loginBG h-screen bg-cover bg-no-repeat bg-center">
            <div className="bg-opacity-015 backdrop-filter backdrop-blur-lg bg-gray md:inset-x-20p xs:top-1p md:top-2p 2xl:top-10p xs:inset-x-10p
            md:w-60p xs:w-80p flex flex-col rounded-3xl relative bg-clip-padding flex flex-col justify-start items-center xs:py-2p md:py-1p">
                <div className="xs:text-sm md:text-xl lg:text-2xl font-bold">No dalej, dołącz do nas!</div>
                <div className="flex flex-col w-90p xs:mx-1p md:mx-5p lg:mx-10p h-50p">
                    <form className="flex flex-col xs:space-y-2 lg:space-y-5 items-center justify-center h-100p w-100p xs:mt-2p lg:mt-5p mx-auto" onSubmit={event => handleRegister(event)}>
                        <div className="w-100p rounded-lg border-2 border-registerBorder flex md:flex-row xs:flex-col flex-wrap justify-between place-content-between p-3 pt-0">
                            <div className="xs:text-xs md:text-lg lg:text-xl w-100p mt-2 text-center">Konto użytkownika</div>
                            <input id="login" name="login" type="text" placeholder="Nazwa użytkownika" required
                                className="shadow-inner xs:w-100p md:w-45p lg:w-40p xs:text-xxs sm:text-lg lg:text-xl xs:h-5 sm:h-7 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5 mt-2" />
                            <input id="mail" name="mail" type="mail" placeholder="Adres E-mail" required
                                className="shadow-inner xs:w-100p md:w-45p lg:w-40p xs:text-xxs sm:text-lg lg:text-xl xs:h-5 sm:h-7 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5 mt-2" />
                            <input id="firstName" name="firstName" type="text" placeholder="Imię" required
                                className="shadow-inner xs:w-100p md:w-45p lg:w-40p xs:text-xxs sm:text-lg lg:text-xl xs:h-5 sm:h-7 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5 mt-2" />
                            <input id="password" name="password" type="password" placeholder="Hasło" required
                                className="shadow-inner xs:w-100p md:w-45p lg:w-40p xs:text-xxs sm:text-lg lg:text-xl xs:h-5 sm:h-7 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5 mt-2" />
                            <input id="lastName" name="lastName" type="text" placeholder="Nazwisko" required
                                className="shadow-inner xs:w-100p md:w-45p lg:w-40p xs:text-xxs sm:text-lg lg:text-xl xs:h-5 sm:h-7 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5 mt-2" />
                            <input id="confirm" name="confirm" type="password" placeholder="Potwierdź hasło" required
                                className="shadow-inner xs:w-100p md:w-45p lg:w-40p xs:text-xxs sm:text-lg lg:text-xl xs:h-5 sm:h-7 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5 mt-2" />
                            <input id="phone" name="phone" type="text" placeholder="Numer telefonu" required
                                className="shadow-inner xs:w-100p md:w-45p lg:w-40p xs:text-xxs sm:text-lg lg:text-xl xs:h-5 sm:h-7 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5 mt-2" />
                        </div>
                        <div className="w-100p rounded-lg border-2 border-registerBorder flex md:flex-row xs:flex-col flex-wrap justify-between place-content-between p-3 pt-0">
                            <div className="xs:text-xs md:text-lg lg:text-xl w-100p mt-2 text-center">Dane firmy</div>
                            <input id="companyName" name="companyName" type="text" placeholder="Nazwa firmy" required
                                className="shadow-inner xs:w-100p md:w-45p lg:w-40p xs:text-xxs sm:text-lg lg:text-xl xs:h-5 sm:h-7 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5 mt-2" />
                            <input id="postCode" name="postCode" type="text" placeholder="Kod pocztowy" required
                                className="shadow-inner xs:w-100p md:w-45p lg:w-40p xs:text-xxs sm:text-lg lg:text-xl xs:h-5 sm:h-7 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5 mt-2" />
                            <input id="city" name="city" type="city" placeholder="Miasto" required
                                className="shadow-inner xs:w-100p md:w-45p lg:w-40p xs:text-xxs sm:text-lg lg:text-xl xs:h-5 sm:h-7 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5 mt-2" />
                            <input id="address" name="address" type="text" placeholder="Ulica" required
                                className="shadow-inner xs:w-100p md:w-45p lg:w-40p xs:text-xxs sm:text-lg lg:text-xl xs:h-5 sm:h-7 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5 mt-2" />
                            <select id="province" name="province" placeholder="Województwo" required
                                className="shadow-inner xs:w-100p md:w-45p lg:w-40p xs:text-xxs sm:text-lg lg:text-xl xs:h-5 sm:h-7 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5 mt-2">
                                <option selected disabled>Województwo</option>
                                <option>dolnośląskie</option>
                                <option>kujawsko-pomorskie</option>
                                <option>lubelskie</option>
                                <option>lubuskie</option>
                                <option>łódzkie</option>
                                <option>małopolskie</option>
                                <option>mazowieckie</option>
                                <option>opolskie</option>
                                <option>podkarpackie</option>
                                <option>podlaskie</option>
                                <option>pomorskie</option>
                                <option>śląskie</option>
                                <option>świętokrzyskie</option>
                                <option>warmińsko-mazurskie</option>
                                <option>wielkopolskie</option>
                                <option>zachodniopomorskie</option>
                            </select>
                            <input id="address2" name="address2" type="text" placeholder="Nr. domu/mieszkania" required
                                className="shadow-inner xs:w-100p md:w-45p lg:w-40p xs:text-xxs sm:text-lg lg:text-xl xs:h-5 sm:h-7 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5 mt-2" />
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

function validate(data, confirmPassword) {
    var isValid = true;
    var message = 'Błędne dane:\r\n'
    const oneUpperCase = new RegExp('[A-Z]+')
    const oneLowerCase = new RegExp('[a-z]+')
    const oneDigit = new RegExp('[0-9]+')
    const minTenChars = new RegExp('.{10,}')
    const postCodePattern = new RegExp('^[0-9]{2}-[0-9]{3}$')

    if (!oneUpperCase.test(data.user.password)) {
        isValid = false;
        message += '-hasło powinno zawierać przynajmniej 1 dużą literę\r\n'
    }
    if (!oneLowerCase.test(data.user.password)) {
        isValid = false;
        message += '-hasło powinno zawierać przynajmniej 1 małą literę\r\n'
    }
    if (!oneDigit.test(data.user.password)) {
        isValid = false;
        message += '-hasło powinno zawierać przynajmniej 1 cyfrę\r\n'
    }

    const regex = /[!\"#\$%&\'()*+,-.\/:;<=>\?\@\]\[\\\{^_`|\}~]+/gm;
    const str = data.user.password;
    let m;
    var isSpecialChar=false;

    while ((m = regex.exec(str)) !== null) {
        // This is necessary to avoid infinite loops with zero-width matches
        if (m.index === regex.lastIndex) {
            regex.lastIndex++;
        }
        
        // The result can be accessed through the `m`-variable.
        m.forEach((match, groupIndex) => {
            
        });
        if(m!==null){
            isSpecialChar=true;
        }
    }

    if (!isSpecialChar) {
        isValid = false;
        message += '-hasło powinno zawierać przynajmniej 1 znak specjalny (!\"#\$%&\'()*+,-./:;<=>\?@\]\[\\\{^_`|\}~)\r\n'
    }

    if (!minTenChars.test(data.user.password)) {
        isValid = false;
        message += '-hasło powinno zawierać przynajmniej 10 znaków\r\n'
    }
    if (data.user.password !== confirmPassword) {
        isValid = false;
        message += '-hasło oraz potwierdzenie hasła powinny być takie same\r\n'
    }
    if (!postCodePattern.test(data.companyAddress.postCode)) {
        isValid = false;
        message += '-kod pocztowy musi być w formacie 00-000\r\n'
    }
    

    if (!isValid) {
        alert(message);
    }

    return isValid;
}