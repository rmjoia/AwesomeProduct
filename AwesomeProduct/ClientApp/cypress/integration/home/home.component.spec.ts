beforeEach(() => {
  cy.visit('/');
});

it('should have correct options on navigation bar', () => {
  cy.get('a.nav-link').eq(0)
    .should('contain.text', 'Home')
    .should('have.attr', 'href', '/');

  cy.get('a.nav-link').eq(1)
    .should('contain.text', 'Process')
    .should('have.attr', 'href', '/batch-process');

  cy.get('a.nav-link').eq(2)
    .should('contain.text', 'History')
    .should('have.attr', 'href', '/history');
});

it('should contain correct Brand', () => {
  cy.get('a.navbar-brand').should('contain.text', 'AwesomeProduct');
});

it('should contain correct Header', () => {
  cy.get('app-home').get('h1').should('contain.text', 'Hello Awesome user!');
});
