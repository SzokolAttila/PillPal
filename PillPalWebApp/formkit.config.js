import {defaultConfig} from '@formkit/vue'
import {generateClasses} from "@formkit/themes";


export default defaultConfig({
    config: {
        classes: generateClasses({
            global: {
                input: 'my-2 form-input rounded-md p-2 w-full border-2 border-component-light dark:border-component-dark',
                label: 'text-textColor-light dark:text-textColor-dark',
                message: 'text-errorColor-light dark:text-errorColor-dark font-bold',
            },
            input: {
                input: 'form-input'
            },
            select: {
                input: 'form-select'
            },
            radio:{
                input: 'form-radio'
            },
            checkbox:{
                input: 'form-checkbox'
            },
            textarea:{
                input: 'form-textarea',
                help: 'text-textColor-light dark:text-textColor-dark'
            },
            submit: {
                wrapper: 'w-fit block mx-auto',
                input: 'bg-component-light dark:bg-component-dark p-2 text-white'

            },
            button: {
                wrapper: 'w-fit block mx-auto',
                input: 'bg-component-light dark:bg-component-dark p-2 text-white'
            }
        })
    }
})
