import { ModalErrorComponentViewModel } from './Error/ModalErrorComponentViewModel';

export class PageFrameViewModel {
    constructor(
        public readonly page: any,
        public readonly modalError: ModalErrorComponentViewModel
    ) {
    }
}