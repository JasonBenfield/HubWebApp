export class ErrorModel implements IErrorModel {
    constructor(public readonly message: string, public readonly propertyName: string = '', public readonly context?: any) {
    }

    toString() {
        let str = '';
        if (this.propertyName) {
            str += `${this.propertyName}, `;
        }
        str += this.message;
        return str;
    }
}