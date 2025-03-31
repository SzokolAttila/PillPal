import {defaultConfig} from '@formkit/vue'
import {generateClasses} from "@formkit/themes";


export default defaultConfig({
    config: {
        classes: generateClasses({
            global: {
                input: 'my-2 form-input rounded-md p-2 w-full',
                label: 'text-textColor-light dark:text-textColor-dark'
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
        })
    }
})
