import { useEffect, useState } from "react";
import "./App.css"

enum Status {
    Success,
    Failed,
    Error
}

class Person {
    name?: string;
    age?: number

    constructor(name: string, age: number) {
        this.name = name;
        this.age = age;
    }
}

declare global {
    let firstSubmit: (input: string) => Promise<null>;
    let secondSubmit: (name: string, age: number) => Promise<null>; 
    let thirdSubmit: () => Promise<null>;
    let fourthSubmit: (status: Status) => Promise<null>; 
    let fifthSubmit: (obj : {result: string}) => Promise<null>;
    let sixthSubmit: (text?: string) => Promise<null>;
    let seventhSubmit: (statuses: Status[]) => Promise<null>;
    let eighthSubmit: (array: boolean[]) => Promise<null>;
    let ninthSubmit: (people: Person[]) => Promise<string>;
    let tenthSubmit: () => Promise<Person[]>;
}

export default function App() {
    const [submitBoolean, setSubmitBoolean] = useState(false);

    function onSubmit() {
        setSubmitBoolean(true);
    }

    useEffect(() => {
        async function submit() {
            if (submitBoolean) {
                await firstSubmit("Hello World");
            
                await secondSubmit("Bob", 23);
                await thirdSubmit();
                await fourthSubmit(Status.Success);
                await fifthSubmit({result: "42"});
                await sixthSubmit(undefined);
                await seventhSubmit([Status.Failed, Status.Success, Status.Error]);
                await eighthSubmit([true, false, true]);

                console.log(await ninthSubmit([new Person("Bob", 32), new Person("Alex", 23)]))
                
                const people = await tenthSubmit();

                for (let i = 0; i < people.length; i++) {
                    console.log(`name: ${people[i].name}`);
                    console.log(`age: ${people[i].age}`);
                }
                
                setSubmitBoolean(false);
            }
        }

        submit();
    }, [submitBoolean])

    return (
    <div className="app">
        <p className="gap-each-submit">firstSubmit("Hello World");</p>
        <p className="gap-each-submit">secondSubmit("Bob", 23);</p>
        <p className="gap-each-submit">thirdSubmit();</p>
        <p className="gap-each-submit">fourthSubmit(Status.Success);</p>
        <p className="gap-each-submit">{`fifthSubmit({result: "42"});`}</p>
        <p className="gap-each-submit">sixthSubmit(undefined)</p>
        <p className="gap-each-submit">seventhSubmit([Status.Failed, Status.Success, Status.Error]);</p>
        <p className="gap-each-submit">eighthSubmit([true, false, true]);</p>
        <p className="gap-each-submit">ninthSubmit([new Person("Bob", 32), new Person("Alex", 23)]);</p>
        <p className="gap-each-submit">await tenthSubmit();</p>

        <button className="communication-input-button gap-each-submit" onClick={onSubmit}>
            <p className="communication-input-button-text">Submit to C#</p>
        </button>
    </div>
  )
}