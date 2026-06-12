console.log("Welcome to Javascript!")

//TODO declare a name variable, a number variable, a boolean variable. fullName, Age, isGraduated.
let fullName = "larry"
let age = 18
let isGraduated = true

//declare an array to contain all the names of students learning fullstack
let students = ["Mavis", "David", "Li"]

//declare a function loop all students and print name

const printName = (students) => {
    for (let i = 1; i <= students.length; i++) {
        console.log(students[i])
    }
}

a(students)

//declare a function to show a alert "clicked" text, go to index.html, add button to bind the function
const onClicked = () => {

}

//declare a object, give person,name,age, greet function

let person = {
    name: 'larry',
    age: 18,
    greet: function () {
        console.log('hello')
    }
}

const originalArray = [1, 2, 3]
const copiedArray = [...originalArray, 4]//Immutable concept

const obj = { name = "larry", age: 18 }
const newObj = { ...obj, job: "IT" }

//destructure
const number1 = originalArray[0]

const [number1] = originalArray
console.log(number1)

const { name, job } = newObj
console.log(name, job)