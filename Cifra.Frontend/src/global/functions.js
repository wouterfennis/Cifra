export { v4 as genUuid } from 'uuid';

/**
 * merge a static string with on usually given as a props.
 *
 * @param {string} initialClass - Props that needs to be skimmed down
 * @param {string|object} extraClass - Props that needs to be skimmed down
 */
export const mergeClass = (initialClass, extraClass) => initialClass + ' ' + extraClass.className ?? extraClass ?? '';  

/**
 * Removes props that are not needed for an element to be rendered.
 *
 * @param {object} props - Props that needs to be skimmed down
 */
export function correctProps(props) {
    const _props = { ...props }
    delete _props?.emptyChild;
    delete _props?.initProps;
    delete _props?.convertKeys;
    delete _props?.wrapperProps;

    return _props
}

/**
 * Calculates the size of given json.
 *
 * @param {JSON} bytes - JSON that needs to be calculated 
 * @param {number} decimals - How many decimals are returned
 */
export function formatBytes(bytes, decimals = 3) {
    if (bytes === 0) return '0 Bytes';

    const k = 1024;
    const dm = decimals < 0 ? 0 : decimals;
    const sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];

    const i = Math.floor(Math.log(bytes) / Math.log(k));

    return parseFloat((bytes / Math.pow(k, i)).toFixed(dm)) + ' ' + sizes[i];
}

/**
 * Checks if value is an function
 *
 * @param {function=} target - Function 
 */
export function isFunc(target){
    return typeof target === "function"
}

/**
 * Check if func = is a function and gives it it's param else return param.
 *
 * @param {function=} func - Function 
 * @param {any} param - Params for function
 */
export function runFunc(func, param){
    return isFunc(func)? func(param): param

}