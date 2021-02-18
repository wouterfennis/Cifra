
import React from 'react'

// initial objects for elements and sections.

// an object concists of the following thing:
// default
// data
// type
// uid
// className
// style

const defaultValue = '';
const emptyChild = true;
export const img = {
    alt: 'https://i.imgur.com/2bvab7y.jpg',
    type: 'img',
    emptyChild,
    convertKeys: {
        src: ['data', 'alt']
    }

}

export const title = {
    defaultValue,
    type: 'h1',
}

export const text = {
    defaultValue,
    type: 'p',
}


export const div = {
    defaultValue,
    type: 'div'
}

export const section = {
    defaultValue,
    className: '',
    type: 'section',
}
export const card = {
    ...div,
    name: "card",
    className: "card",
    wrapperProps: {
        className: 'horizontal center'
    },
    initProps: {
        elements: [
            { ...img, translate: "image" },
            {
                ...section,
                initProps:{
                    elements: [
                        { ...title, translate: "title" },
                        { ...text, translate: "content" },
                    ]
                }
            }
        ]
    }
}
export const elementsMap = {
    div,
    section,
    text,
    title,
    img
}
const inputMap = {
    input: ['h1', 'img'],
    textArea: ['p'],

}
const Input = (props) =>
    <input name={props.uid} type="text" {...props} />

const TextArea = (props) =>
    <textarea name={props.uid} type="text" {...props} />


const inputComponent = {
    input: Input,
    textArea: TextArea
}

export const getInput = (type) => {
    const inputType = Object.keys(inputMap).find((key) => {
        return inputMap[key].find(item => item === type) ? key : null
    })
    return inputComponent[inputType] ?? null
}