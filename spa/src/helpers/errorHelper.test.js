const errorHelper = require("./errorHelper")
// @ponicode
describe("errorHelper.formatErrorResponse", () => {
    test("0", () => {
        let callFunction = () => {
            errorHelper.formatErrorResponse({ response: 500 })
        }
    
        expect(callFunction).not.toThrow()
    })

    test("1", () => {
        let callFunction = () => {
            errorHelper.formatErrorResponse({ response: 429 })
        }
    
        expect(callFunction).not.toThrow()
    })

    test("2", () => {
        let callFunction = () => {
            errorHelper.formatErrorResponse({ response: 400 })
        }
    
        expect(callFunction).not.toThrow()
    })

    test("3", () => {
        let callFunction = () => {
            errorHelper.formatErrorResponse({ response: 404 })
        }
    
        expect(callFunction).not.toThrow()
    })

    test("4", () => {
        let callFunction = () => {
            errorHelper.formatErrorResponse({ response: 200 })
        }
    
        expect(callFunction).not.toThrow()
    })

    test("5", () => {
        let callFunction = () => {
            errorHelper.formatErrorResponse({ response: NaN })
        }
    
        expect(callFunction).not.toThrow()
    })
})
