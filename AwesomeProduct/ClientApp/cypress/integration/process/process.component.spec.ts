beforeEach(() => {
  cy.visit('/batch-process');
});

it('should have correct title', () => {
  cy.get('h1')
    .should('contain.text', 'Process Things...');
});

it('should have the correct lead', () => {
  cy.get('p')
    .should('have.text', 'Enter the data and click process to process things.');
});

it('should have batches label', () => {
  cy.get('label').eq(0)
    .should('have.text', 'Batches');
});

it('should have batches input', () => {
  cy.get('input').eq(0).should('be.visible');
});

it('should have numbers label', () => {
  cy.get('label').eq(1)
    .should('have.text', 'Numbers / Batch');
});

it('should have batches input', () => {
  cy.get('input').eq(1).should('be.visible');
});

it('should not display process button when inputs are zeroed', () => {
  cy.get('input').eq(0).should('have.value', 0);
  cy.get('input').eq(1).should('have.value', 0);
  cy.get('.button .button-primary').should('not.exist');
});

it('should display process button when inputs are valid', () => {
  cy.get('input').eq(0).should('have.value', 0);
  cy.get('input').eq(1).should('have.value', 0);
  cy.get('.button .button-primary').should('not.exist');

  cy.get('input').eq(0).type('1');
  cy.get('input').eq(1).type('1')
  cy.get('.button.btn-primary').should('exist');
});

it('should not display processing message while not processing', () => {
  cy.get('.alert.alert-primary').should('not.exist');
});

it('should display processing message while processing', () => {
  cy.get('.alert.alert-primary').should('not.exist');

  cy.get('input').eq(0).type('1');
  cy.get('input').eq(1).type('1')

  cy.get('.button.btn-primary').click();

  cy.get('.alert.alert-primary').should('exist');
});


